using Cinema_project_MVC.Data;
using Microsoft.AspNetCore.Mvc;

namespace Cinema_project_MVC.Controllers
{
    public class ActorController : Controller
    {
        AppDbContext db = new AppDbContext();
        public IActionResult Index(int Id)
        {
var actor= db.actors.Find(Id);
            return View(actor);
        }

    }
}
