using Cinema_project_MVC.Data;
using Cinema_project_MVC.Models;
using Cinema_project_MVC.Repository.IReprsitory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema_project_MVC.Controllers
{
    public class CategoryController : Controller
    {

        ICatrgoryRepository catrgoryRepository;
        public CategoryController(ICatrgoryRepository catrgoryRepository)
        {
            this.catrgoryRepository = catrgoryRepository;
        }

        public IActionResult Detailes(int Id)
        {


            var moves = catrgoryRepository.Getone(new Func<IQueryable<Category>, IQueryable<Category>>[]
            {
               q=>q.Include(p=>p.Movies).ThenInclude(p=>p.Cinema)
            },
            filter:e=>e.Id==Id


                );
            return View(moves);
        }

        public IActionResult Index()
        {

            var categorise= catrgoryRepository.GetAll(new Func<IQueryable<Category>, IQueryable<Category>>[]
            {
                 q=>q.Include(p=>p.Movies).ThenInclude(p=>p.Cinema)
            });
            return View(categorise);
        }
    }
}
