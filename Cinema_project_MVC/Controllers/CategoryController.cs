using Cinema_project_MVC.Data;
using Cinema_project_MVC.Models;
using Cinema_project_MVC.Repository;
using Cinema_project_MVC.Repository.IReprsitory;
using Cinema_project_MVC.StaticData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema_project_MVC.Controllers
{
    [Authorize(Roles = $"{SD.AdminRole},{SD.CompanyRole}")]
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
            return View(categorise.ToList());
        }
        public IActionResult Index2(int page, string? search)
        {

            var categorise = catrgoryRepository.GetAll(new Func<IQueryable<Category>, IQueryable<Category>>[]
            {
                
            });


            if(page <= 1)
                page = 1;
            int pageSize = 5;
            int totalProducts = catrgoryRepository.GetAll().Count();

            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            if (search != null)
            {
                search = search.TrimStart();
                search = search.TrimEnd();
                categorise = categorise.Where(e=>e.Name.Contains(search));
            }

            categorise= categorise.Skip((page-1)*5).Take(5);
            return View(categorise.ToList());
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
