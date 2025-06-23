using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Cinema_project_MVC.Models;
using Cinema_project_MVC.Data;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using Cinema_project_MVC.Repository.IReprsitory;

namespace Cinema_project_MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

   // AppDbContext db = new AppDbContext();
   IMovieRepository movieRepository;

 
    public HomeController(ILogger<HomeController> logger, IMovieRepository movieRepository)
    {
        _logger = logger;
        this.movieRepository = movieRepository;
    }

    public IActionResult Index(string searchTerm)
    {
        //var movies = db.movies
        //               .Include(m => m.Cinema)
        //               .Include(m => m.Category)
        //               .AsQueryable();
        var movies = movieRepository.GetAll(new Func<IQueryable<Movie>, IQueryable<Movie>>[]
        {
            q=>q.Include(p=>p.Cinema)
            .Include(p=>p.Category).AsQueryable()

        });

        if (!string.IsNullOrEmpty(searchTerm))
        {

            movies = movies.Where(m => m.Name.Contains(searchTerm));

            if (!movies.Any())
            {
              
                return RedirectToAction("NotFountPage");
            }
        }

        return View(movies.ToList());
    }

    public IActionResult MoreDetailes(int Id)
    {
        // var movies = db.movies.Include(m=>m.Cinema).Include(m=>m.Category).Include(m=>m.ActorMovie).ThenInclude(m => m.Actor).FirstOrDefault(m=>m.Id==Id);
        var movies = movieRepository.Getone(new Func<IQueryable<Movie>, IQueryable<Movie>>[]
        {
            q=>q.Include(p=>p.Cinema).Include(p=>p.Category).Include(p=>p.ActorMovie).ThenInclude(p=>p.Actor)

        },
        filter: e => e.Id ==Id 
        );


            if (movies != null)
        {
            return View(movies);
        }
        else
        {
          return  RedirectToAction("NotFountPage");
        }
    }
    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult NotFountPage()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
