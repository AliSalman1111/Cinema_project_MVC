using Cinema_project_MVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema_project_MVC.Controllers
{
    public class CategoryController : Controller
    {
        AppDbContext db = new AppDbContext();

        public IActionResult Detailes(int Id)
        {
            var moves = db.categories.Include(m => m.Movies).ThenInclude(m=>m.Cinema).FirstOrDefault(m => m.Id == Id);
            return View(moves);
        }

        public IActionResult Index()
        {
            var categorise =db.categories. Include(c=>c.Movies).ThenInclude(c=>c.Cinema).ToList();
            return View(categorise);
        }
    }
}
