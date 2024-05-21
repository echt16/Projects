using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project.Models;

namespace Project.Pages
{
    public class RoomsModel : PageModel
    {
        public QuestRoomContext context;
        public string nameSort = "";
        public RoomsModel(QuestRoomContext db)
        {
            context = db;
        }
        public void OnGet(string name = "")
        {
            nameSort = name;
        }
    }
}
