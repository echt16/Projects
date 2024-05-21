using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project.Models;

namespace Project.Pages
{
    public class ChangePhotosModel : PageModel
    {
        public QuestRoomContext context;
        public ChangePhotosModel(QuestRoomContext db)
        {
            context = db;
        }
        public void OnPostAddphoto(string room, string photo)
        {
            int tId = int.Parse(room.Split('.')[0]);
            context.Photos.Add(new Photo()
            {
                QuestRoomId = tId,
                Src = photo
            });
            context.SaveChanges();
        }
        public void OnPostDeletephoto(string photo)
        {
            int tId = int.Parse(photo.Split('.')[0]);
            context.Photos.Remove(context.Photos.First(i => i.Id == tId));
            context.SaveChanges();
        }
    }
}
