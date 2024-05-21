using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project.Models;

namespace Project.Pages
{
    public class ChangeCompaniesModel : PageModel
    {
        public QuestRoomContext context;
        public ChangeCompaniesModel(QuestRoomContext db)
        {
            context = db;
        }

        public void OnPostAddcomp(string name, string email)
        {
            Company c = new Company()
            {
                Name = name,
                Email = email
            };
            context.Companies.Add(c);
            context.SaveChanges();
        }
        public void OnPostChangecomp(string id, string name, string email)
        {
            int tId = int.Parse(id.Split(".")[0]);
            context.Companies.First(c => c.Id == tId).Name = name;
            context.Companies.First(c => c.Id == tId).Email = email;
            context.SaveChanges();
        }
        public void OnPostDeletecomp(string id)
        {
            int tId = int.Parse(id.Split(".")[0]);
            foreach (PhoneNumber p in context.PhoneNumbers.Where(c => c.CompanyId == tId))
            {
                context.PhoneNumbers.Remove(p);
            }
            context.SaveChanges();

            foreach (QuestRoom q in context.QuestRooms.Where(i => i.CompanyId == tId))
            {
                foreach (Photo r in q.Photos) 
                {
                    context.Photos.Remove(r);
                }
                //context.SaveChanges();

                context.QuestRooms.Remove(q);
            }
            context.Companies.Remove(context.Companies.First(i => i.Id == tId));
            context.SaveChanges();
        }
    }
}
