using demo_hero_complete.Models;
using Microsoft.EntityFrameworkCore;
namespace demo_hero_complete.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<SuperHero> SuperHeroes { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Seed initial data
        //    modelBuilder.Entity<SuperHero>().HasData(
        //        new SuperHero { Id = 1, Name = "Superman", Description = "Man of Steel", Type = "Hero", Planet = "Krypton" },
        //        new SuperHero { Id = 2, Name = "Batman", Description = "Dark Knight", Type = "Hero", Planet = "Earth" }
        //    );
        //}
    }
}

