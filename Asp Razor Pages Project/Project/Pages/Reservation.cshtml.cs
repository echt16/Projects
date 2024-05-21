using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using Project.Models;

namespace Project.Pages
{
    public class ReservationModel : PageModel
    {
        private QuestRoomContext Context { get; set; }
        public List<QuestRoom> QuestRooms { get; set; }
        public List<Reservation> Reservations { get; set; }
        public QuestRoom SelectedRoom { get; set; }
        public DateTime Day { get; set; }
        public bool IsFilled { get; set; } = false;

        public ReservationModel(QuestRoomContext db)
        {
            Context = db;
            QuestRooms = Context.QuestRooms.ToList();
            SelectedRoom = new QuestRoom()
            {
                TravelTime = new TimeSpan(0, 0, 0)
            };
            Day = new DateTime(1, 1, 1);
        }

        public void OnPost(int room = -1, DateTime date = new DateTime())
        {
            QuestRooms = Context.QuestRooms.ToList();
            Reservations = Context.Reservations.Where(x => x.QuestRoomId == room && x.Starts.Year == date.Year && x.Starts.Month == date.Month && x.Starts.Day == date.Day).ToList();
            SelectedRoom = Context.QuestRooms.FirstOrDefault(x => x.Id == room);
            Day = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            IsFilled = true;
            SetCookie("room", room.ToString(), 1);
            SetCookie("day", date.ToString(@"yyyy.MM.dd"), 1);
            if (QuestRooms.Count > 0)
            {
                (QuestRooms[0], QuestRooms[QuestRooms.FindIndex(x => x.Id == room)]) = (QuestRooms[QuestRooms.FindIndex(x => x.Id == room)], QuestRooms[0]);
            }
        }

        public int IncludedInTheDate(DateTime date)
        {
            List<Reservation> reservations = Reservations.ToList();
            string login = GetCookie("user").Split("^")[0];
            for (int i = 0; i < reservations.Count; i++)
            {
                if (date >= reservations[i].Starts && date < (reservations[i].Starts + SelectedRoom.TravelTime))
                {
                    if (reservations[i].UserId == Context.Users.FirstOrDefault(x => x.Login == login).Id)
                    {
                        return 0;
                    }
                    return 1;
                }
            };
            return -1;
        }

        public async Task<RedirectToPageResult> OnPostCheck(string[] times)
        {
            foreach (string item in times)
            {
                string roomStr = GetCookie("room");
                string[] dayStr = GetCookie("day").Split(".");
                int room = -1;
                if (times.Length > 0 && !roomStr.IsNullOrEmpty() && dayStr.Length > 0)
                {
                    foreach (string time in times)
                    {
                        room = int.Parse(roomStr);
                        Day = new DateTime(int.Parse(dayStr[0]), int.Parse(dayStr[1]), int.Parse(dayStr[2]));
                        Day = Day.AddHours(int.Parse(time.Split(":")[0]));
                        string login = GetCookie("user").Split("^")[0];
                        Context.Reservations.Add(new Reservation()
                        {
                            QuestRoomId = room,
                            UserId = Context.Users.First(x => x.Login == login).Id,
                            Starts = Day
                        });
                        await Context.SaveChangesAsync();
                    }
                    return RedirectToPage("/Index");
                }
            }
            return RedirectToPage("/Reservation");
        }

        public void SetCookie(string cookieName, string cookieValue, int daysToExpire)
        {
            CookieOptions co = new CookieOptions();
            co.Expires = DateTimeOffset.Now.AddDays(daysToExpire);
            Response.Cookies.Append(cookieName, cookieValue, co);
        }

        public string GetCookie(string cookieName)
        {
            string cookie = Request.Cookies[cookieName];

            if (!cookie.IsNullOrEmpty())
            {
                return cookie;
            }
            else
            {
                return "";
            }
        }
    }
}
