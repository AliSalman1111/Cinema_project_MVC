using Cinema_project_MVC.Data;
using Cinema_project_MVC.Repository;
using Cinema_project_MVC.Repository.IReprsitory;
using Microsoft.EntityFrameworkCore;

namespace Cinema_project_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(
              option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
              );
            builder.Services.AddScoped< ICatrgoryRepository, CategotyRepository>();

            builder.Services.AddScoped<ICenimaRepository, CinemaRepository>();
         // builder.Services.AddScoped<ICinemaRepository, CinemaRepository>();


            builder.Services.AddScoped<IMovieRepository, MovieRpository>();
            builder.Services.AddScoped<IMovieActor, MonieActor>();
            builder.Services.AddScoped<IActorRepository, ActorRepositury>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
