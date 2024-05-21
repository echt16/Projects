using Microsoft.EntityFrameworkCore;

namespace Project.Models
{
    public class QuestRoomContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<QuestRoom> QuestRooms { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public QuestRoomContext(DbContextOptions<QuestRoomContext> options) : base(options) { Database.EnsureCreated(); }
    }
}
