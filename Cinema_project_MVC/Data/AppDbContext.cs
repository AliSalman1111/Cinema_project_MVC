using Cinema_project_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema_project_MVC.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions)

              : base(dbContextOptions)

        {

        }

        public DbSet<Cinema> cinemas {  get; set; }
        public DbSet<Movie> movies { get; set; }
        public DbSet<Actor> actors { get; set; }

        public DbSet<ActorMovie> actorsMovie { get; set; }
        public DbSet<Category> categories { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ActorMovie>()
       .HasKey(am => new { am.ActorId, am.MovieId });
        }
    }
}
