using Cinema_project_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema_project_MVC.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Cinema> cinemas {  get; set; }
        public DbSet<Movie> movies { get; set; }
        public DbSet<Actor> actors { get; set; }

        public DbSet<ActorMovie> actorsMovie { get; set; }
        public DbSet<Category> categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var connection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connection);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ActorMovie>()
       .HasKey(am => new { am.ActorId, am.MovieId });
        }
    }
}
