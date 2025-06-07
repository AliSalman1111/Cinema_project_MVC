using Cinema_project_MVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema_project_MVC.Controllers
{
    public class CinemaController : Controller
    {
        AppDbContext db = new AppDbContext();
        public IActionResult Index()
        {
            var cinemas=db.cinemas.Include(c=>c.Movies).ThenInclude(c=>c.Category).ToList();
            return View(cinemas);
        }
        public IActionResult Detailes(int Id)
        {
            var moves = db.cinemas.Include(m => m.Movies).ThenInclude(m => m.Category).FirstOrDefault(m => m.Id == Id);
            return View(moves);
        }
    }
}
