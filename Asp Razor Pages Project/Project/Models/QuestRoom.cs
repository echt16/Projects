namespace Project.Models
{
    public class QuestRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan TravelTime { get; set; }
        public byte MinPlayer {  get; set; }
        public byte MaxPlayer {  get; set; }
        public byte MinAge {  get; set; }
        public string Address {  get; set; }
        public int CompanyId {  get; set; }
        public Company Company { get; set; }
        public byte Rating {  get; set; }
        public byte FearLevel {  get; set; }
        public byte DifficultyLevel {  get; set; }
        public string LogoSrc { get; set; }
        public virtual List<Photo> Photos { get; set; }
        public virtual List<Reservation> Reservations { get; set; }
        public QuestRoom()
        {
            Photos = new List<Photo>();
            Reservations = new List<Reservation>();
        }
    }
}
