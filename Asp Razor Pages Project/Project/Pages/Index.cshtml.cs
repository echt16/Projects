using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using Project.Models;
using System;

namespace Project.Pages
{
    public class IndexModel : PageModel
    {
        public QuestRoomContext Context { get; set; }
        public string SortBy { get; set; } = "Rating";
        public bool SortAsc { get; set; } = true;
        public List<QuestRoom> QuestRooms { get; set; }
        public List<string> SortValues { get; set; } = new List<string>()
        {
            "Rating",
            "Name",
            "TravelTime",
            "MinAge",
            "FearLevel",
            "DifficultyLevel",
            "MinPlayer",
            "MaxPlayer"
        };

        public IndexModel(QuestRoomContext db)
        {
            Context = db;
            /*Context.Companies.AddRange(new List<Company>()
            {
                new()
                {
                     Name = "comp1",
                      Email="email1"
                },
                new()
                {
                     Name = "comp2",
                      Email="email2"
                },
                new()
                {
                     Name = "comp3",
                      Email="email3"
                },
            });
            Context.SaveChanges();
            Context.PhoneNumbers.AddRange(new List<PhoneNumber>()
            {
                new()
                {
                     Number = "1",
                      CompanyId = 1
                },
                new()
                {
                     Number = "2",
                      CompanyId = 2
                },
                new()
                {
                     Number = "3",
                      CompanyId = 3
                }
            });
            Context.SaveChanges();
            Context.QuestRooms.AddRange(new List<QuestRoom>()
            {
                new()
                {
                     Address = "address1", CompanyId = 1, Description = "descr1", DifficultyLevel = 1, FearLevel = 1, LogoSrc = "https://t3.ftcdn.net/jpg/05/67/29/94/360_F_567299489_7Ndvhfu0v3CZvOqBoOk4BzLyP1BdlWm9.jpg", MaxPlayer = 1, MinPlayer = 1, MinAge = 1, Name = "room1",Rating = 1, TravelTime = new TimeSpan(1, 0, 0)
                },
                new()
                {
                     Address = "address2", CompanyId = 2, Description = "descr2", DifficultyLevel = 1, FearLevel = 2, LogoSrc = "https://t3.ftcdn.net/jpg/05/67/29/94/360_F_567299489_7Ndvhfu0v3CZvOqBoOk4BzLyP1BdlWm9.jpg", MaxPlayer = 2, MinPlayer = 2, MinAge = 2, Name = "room2",Rating = 2, TravelTime = new TimeSpan(2, 0, 0)
                },
                new()
                {
                     Address = "address3", CompanyId = 3, Description = "descr3", DifficultyLevel = 3, FearLevel = 3, LogoSrc = "https://t3.ftcdn.net/jpg/05/67/29/94/360_F_567299489_7Ndvhfu0v3CZvOqBoOk4BzLyP1BdlWm9.jpg", MaxPlayer = 3, MinPlayer = 3, MinAge = 3, Name = "room3",Rating = 3, TravelTime = new TimeSpan(3, 0, 0)
                }
            });
            Context.SaveChanges();
            Context.Photos.AddRange(new List<Photo>()
            {
                new()
                {
                     Src = "https://i.pinimg.com/736x/f0/b0/30/f0b030f6ff4adb617207e6169e5639cb.jpg", QuestRoomId = 1
                },
                new()
                {
                     Src = "https://png.pngtree.com/thumb_back/fh260/background/20220225/pngtree-splash-magenta-background-image_1035659.jpg", QuestRoomId = 2
                },
                new()
                {
                     Src = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTb7n80RbkFKEE8KcvgSSlfHDWgbikp13FWVhB-n6ZLK9nd1GwA1iuC8tEBH4N99YJl7Pc&usqp=CAU", QuestRoomId = 3
                },
            });
            Context.SaveChanges();*/
        }

        public void OnGet(string sortValue = "")
        {
            string asc = GetCookieValue("asc");
            string travelTimeFrom = GetCookieValue("travelTimeFrom");
            string minAgeFrom = GetCookieValue("minAgeFrom");
            string ratingFrom = GetCookieValue("ratingFrom");
            string fearLevelFrom = GetCookieValue("fearLevelFrom");
            string difficultyLevelFrom = GetCookieValue("difficultyLevelFrom");
            string minPlayerFrom = GetCookieValue("minPlayerFrom");
            string maxPlayerFrom = GetCookieValue("maxPlayerFrom");
            string travelTimeTo = GetCookieValue("travelTimeTo");
            string minAgeTo = GetCookieValue("minAgeTo");
            string ratingTo = GetCookieValue("ratingTo");
            string fearLevelTo = GetCookieValue("fearLevelTo");
            string difficultyLevelTo = GetCookieValue("difficultyLevelTo");
            string minPlayerTo = GetCookieValue("minPlayerTo");
            string maxPlayerTo = GetCookieValue("maxPlayerTo");
            QuestRooms = Context.QuestRooms.ToList();

            if (!travelTimeFrom.IsNullOrEmpty())
            {
                TimeSpan timeSpan = new TimeSpan(int.Parse(travelTimeFrom.Split(":")[0]), int.Parse(travelTimeFrom.Split(":")[1]), 0);
                QuestRooms = QuestRooms.Where(x => x.TravelTime >= timeSpan).ToList();
            }

            if (!travelTimeTo.IsNullOrEmpty())
            {
                TimeSpan timeSpan = new TimeSpan(int.Parse(travelTimeTo.Split(":")[0]), int.Parse(travelTimeTo.Split(":")[1]), 0);
                QuestRooms = QuestRooms.Where(x => x.TravelTime <= timeSpan).ToList();
            }

            if (!minAgeFrom.IsNullOrEmpty())
            {
                QuestRooms = QuestRooms.Where(x => x.MinAge >= int.Parse(minAgeFrom)).ToList();
            }

            if (!minAgeTo.IsNullOrEmpty())
            {
                QuestRooms = QuestRooms.Where(x => x.MinAge <= int.Parse(minAgeTo)).ToList();
            }

            if (!ratingFrom.IsNullOrEmpty())
            {
                QuestRooms = QuestRooms.Where(x => x.Rating >= int.Parse(ratingFrom)).ToList();
            }

            if (!ratingTo.IsNullOrEmpty())
            {
                QuestRooms = QuestRooms.Where(x => x.Rating <= int.Parse(ratingTo)).ToList();
            }

            if (!fearLevelFrom.IsNullOrEmpty())
            {
                QuestRooms = QuestRooms.Where(x => x.FearLevel >= int.Parse(fearLevelFrom)).ToList();
            }

            if (!fearLevelTo.IsNullOrEmpty())
            {
                QuestRooms = QuestRooms.Where(x => x.FearLevel <= int.Parse(fearLevelTo)).ToList();
            }

            if (!difficultyLevelFrom.IsNullOrEmpty())
            {
                QuestRooms = QuestRooms.Where(x => x.DifficultyLevel >= int.Parse(difficultyLevelFrom)).ToList();
            }

            if (!difficultyLevelTo.IsNullOrEmpty())
            {
                QuestRooms = QuestRooms.Where(x => x.DifficultyLevel <= int.Parse(difficultyLevelTo)).ToList();
            }

            if (!minPlayerFrom.IsNullOrEmpty())
            {
                QuestRooms = QuestRooms.Where(x => x.MinPlayer >= int.Parse(minPlayerFrom)).ToList();
            }

            if (!minPlayerTo.IsNullOrEmpty())
            {
                QuestRooms = QuestRooms.Where(x => x.MinPlayer <= int.Parse(minPlayerTo)).ToList();
            }

            if (!maxPlayerFrom.IsNullOrEmpty())
            {
                QuestRooms = QuestRooms.Where(x => x.MaxPlayer >= int.Parse(maxPlayerFrom)).ToList();
            }

            if (!maxPlayerTo.IsNullOrEmpty())
            {
                QuestRooms = QuestRooms.Where(x => x.MaxPlayer <= int.Parse(maxPlayerTo)).ToList();
            }



            if (asc == "true" || asc == "")
            {
                SortAsc = true;
            }
            else
            {
                SortAsc = false;
            }

            SortBy = sortValue;
            if (SortBy != "")
                (SortValues[0], SortValues[SortValues.IndexOf(SortBy)]) = (SortValues[SortValues.IndexOf(SortBy)], SortValues[0]);
        }

        public string GetCookieValue(string cookieName)
        {
            string? cookie = Request.Cookies[cookieName];
            if (cookie != null)
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
