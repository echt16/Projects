using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Project.Models;

namespace Project.Pages
{
    public class ChangeRoomsModel : PageModel
    {
        public QuestRoomContext context;
        public ChangeRoomsModel(QuestRoomContext db)
        {
            context = db;
        }
        public void OnPostAddroom(string name, string desc, string logo, string time, byte min, byte max, byte age, string adress, byte rating, byte fear, byte difficulty, string company)
        {
            List<int> ts = time.Split(':').ToList().Select(i => int.Parse(i)).ToList();

            QuestRoom q = new QuestRoom()
            {
                Name = name,
                Description = desc,
                LogoSrc = logo,
                TravelTime = new TimeSpan(ts[0], ts[1], ts[2]),
                MinPlayer = min,
                MaxPlayer = max,
                Address = adress,
                MinAge = age,
                Rating = rating,
                FearLevel = fear,
                DifficultyLevel = difficulty,
                CompanyId = int.Parse(company.Split('.').ToList()[0])
            };

            context.QuestRooms.Add(q);
            context.SaveChanges();
        }
        public void OnPostChangeroom(string id, string name, string desc, string logo, string time, byte min, byte max, byte age, string adress, byte rating, byte fear, byte difficulty, string company)
        {
            List<int> ts = time.Split(':').ToList().Select(i => int.Parse(i)).ToList();
            int tId = int.Parse(id.Split(".")[0]); 

            QuestRoom q = new QuestRoom()
            {
                Name = name,
                Description = desc,
                LogoSrc = logo,
                TravelTime = new TimeSpan(ts[0], ts[1], ts[2]),
                MinPlayer = min,
                MaxPlayer = max,
                Address = adress,
                MinAge = age,
                Rating = rating,
                FearLevel = fear,
                DifficultyLevel = difficulty,
                CompanyId = int.Parse(company.Split('.').ToList()[0])
            };

            context.QuestRooms.First(i => i.Id == tId).Name = q.Name;
            context.QuestRooms.First(i => i.Id == tId).Description = q.Description;
            context.QuestRooms.First(i => i.Id == tId).LogoSrc = q.LogoSrc;
            context.QuestRooms.First(i => i.Id == tId).TravelTime = q.TravelTime;
            context.QuestRooms.First(i => i.Id == tId).MinPlayer = q.MinPlayer;
            context.QuestRooms.First(i => i.Id == tId).MaxPlayer = q.MaxPlayer;
            context.QuestRooms.First(i => i.Id == tId).Address = q.Address;
            context.QuestRooms.First(i => i.Id == tId).MinAge = q.MinAge;
            context.QuestRooms.First(i => i.Id == tId).Rating = q.Rating;
            context.QuestRooms.First(i => i.Id == tId).FearLevel = q.FearLevel;
            context.QuestRooms.First(i => i.Id == tId).DifficultyLevel = q.DifficultyLevel;
            context.QuestRooms.First(i => i.Id == tId).CompanyId = q.CompanyId;

            context.SaveChanges();
        }

        public void OnPostDeleteroom(string id)
        {
            int tId = int.Parse(id.Split(".")[0]);
            foreach (Photo p in context.Photos.Where(i => i.QuestRoomId == tId))
            {
                context.Photos.Remove(p);
            }
            context.SaveChanges();

            context.QuestRooms.Remove(context.QuestRooms.First(i => i.Id == tId));
            context.SaveChanges();
        }
    }
}
