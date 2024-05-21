using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project.Models;

namespace Project.Pages
{
    public class ChangeNumbersModel : PageModel
    {
        public QuestRoomContext context;
        public ChangeNumbersModel(QuestRoomContext db)
        {
            context = db;
        }
        public void OnPostAddnumber(string company, string number)
        {
            int tId = int.Parse(company.Split('.')[0]);
            context.PhoneNumbers.Add(new PhoneNumber()
            {
                CompanyId = tId,
                Number = number
            });
            context.SaveChanges();
        }
        public void OnPostDeletenumber(string number)
        {
            int tId = int.Parse(number.Split('.')[0]);
            context.PhoneNumbers.Remove(context.PhoneNumbers.First(i => i.Id == tId));
            context.SaveChanges();
        }
    }
}
