namespace Project.Models
{
    public class PhoneNumber
    {
        public int Id { get; set; }
        public string Number {  get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
