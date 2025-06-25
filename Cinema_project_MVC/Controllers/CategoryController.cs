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
        public IActionResult Index2()
        {

            var categorise = catrgoryRepository.GetAll(new Func<IQueryable<Category>, IQueryable<Category>>[]
            {
                
            });
            return View(categorise);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Category category)
        {
            catrgoryRepository.Add(category);
            catrgoryRepository.Commit();

            return RedirectToAction("Index2");
        }

        public IActionResult Update(int Id)
        {
            var Category = catrgoryRepository.Getone(new Func<IQueryable<Category>, IQueryable<Category>>[]
           {
             
           },
           filter: e => e.Id == Id


               );
            return View(Category);
        }

           
        
        [HttpPost]
        public IActionResult Update(Category category)
        {
            catrgoryRepository.Edit(category);
            catrgoryRepository.Commit();

            return RedirectToAction("Index2");
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var category = catrgoryRepository.Getone(new Func<IQueryable<Category>, IQueryable<Category>>[]
           {

           },
           filter: e => e.Id == Id


               );

            if (category != null)
            {
                catrgoryRepository.Delete(category);
                catrgoryRepository.Commit();
            }

            return RedirectToAction("Index2");
        }



    }
}
