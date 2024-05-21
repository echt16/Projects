namespace Project.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int QuestRoomId {  get; set; }
        public QuestRoom QuestRoom { get; set; }
        public DateTime Starts { get; set; }
    }
}
