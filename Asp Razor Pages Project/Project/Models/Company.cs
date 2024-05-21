namespace Project.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email {  get; set; }
        public virtual List<PhoneNumber> PhoneNumbers { get; set; }
        public virtual List<QuestRoom> QuestRooms { get; set; }
        public Company()
        {
            PhoneNumbers = new List<PhoneNumber>();
            QuestRooms = new List<QuestRoom>();
        }
    }
}
