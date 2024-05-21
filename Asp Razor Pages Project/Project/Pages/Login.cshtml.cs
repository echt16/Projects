using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project.Models;

namespace Project.Pages
{
    public class LoginModel : PageModel
    {
        public QuestRoomContext context;
        public LoginModel(QuestRoomContext db)
        {
            context = db;
        }
        public void OnGet()
        {
        }
    }
}
