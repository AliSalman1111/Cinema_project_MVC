using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Cinema_project_MVC.Models;
using Cinema_project_MVC.Data;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace Cinema_project_MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    AppDbContext db = new AppDbContext();
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(string searchTerm)
    {
        var movies = db.movies
                       .Include(m => m.Cinema)
                       .Include(m => m.Category)
                       .AsQueryable();

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
        var movies = db.movies.Include(m=>m.Cinema).Include(m=>m.Category).Include(m=>m.ActorMovie).ThenInclude(m => m.Actor).FirstOrDefault(m=>m.Id==Id);

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
