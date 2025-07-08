using Cinema_project_MVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Cinema_project_MVC.modelView;

namespace Cinema_project_MVC.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
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

        public DbSet<cart> carts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ActorMovie>()
       .HasKey(am => new { am.ActorId, am.MovieId });
            modelBuilder.Entity<cart>()
                  .HasKey(c => new { c.movieId, c.ApplicationUserId });
        }
        
    }
}
