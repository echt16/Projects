﻿namespace Project.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public virtual List<Reservation> Reservations { get; set; }
        public User()
        {
            Reservations = new List<Reservation>();
        }
    }
}
