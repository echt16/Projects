using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cw
{
    internal class OlympiadDBContext : DbContext
    {
        public OlympiadDBContext() : base("DBConn") { }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Olympiad> Olympiads { get; set; }
        public DbSet<SportType> SportTypes { get; set; }
        public DbSet<SportTypeOlympiad> SportTypeOlympiads { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<City>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<City>().Property(x => x.CountryId).IsRequired();
            modelBuilder.Entity<Member>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Member>().Property(x => x.Surname).IsRequired();
            modelBuilder.Entity<Member>().Property(x => x.Patronymic).IsRequired();
            modelBuilder.Entity<Member>().Property(x => x.SportTypeId).IsRequired();
            modelBuilder.Entity<Member>().Property(x => x.Birthday).IsRequired();
            modelBuilder.Entity<Member>().Property(x => x.BusyPlace).IsRequired();
            modelBuilder.Entity<Member>().Property(x => x.CountryId).IsRequired();
            modelBuilder.Entity<Olympiad>().Property(x => x.CityId).IsRequired();
            modelBuilder.Entity<Olympiad>().Property(x => x.IsSummerly).IsRequired();
            modelBuilder.Entity<Olympiad>().Property(x => x.Year).IsRequired();
            modelBuilder.Entity<SportType>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<SportTypeOlympiad>().Property(x => x.SportTypeId).IsRequired();
            modelBuilder.Entity<SportTypeOlympiad>().Property(x => x.OlympiadId).IsRequired();
            modelBuilder.Entity<Olympiad>().HasRequired(x => x.City).WithMany().WillCascadeOnDelete(true);
            base.OnModelCreating(modelBuilder);
        }
    }
}
