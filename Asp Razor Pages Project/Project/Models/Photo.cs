namespace Project.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Src { get; set; }
        public int QuestRoomId {  get; set; }
        public QuestRoom QuestRoom { get; set; }
    }
}
