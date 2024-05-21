using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project.Models;

namespace Project.Pages
{
    public class RoomModel : PageModel
    {
        public QuestRoomContext context;
        public RoomModel(QuestRoomContext db)
        {
            context = db;
        }
        public void OnGet()
        {
        }
    }
}
