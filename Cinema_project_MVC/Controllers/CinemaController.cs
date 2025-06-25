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
    [Authorize(Roles = $"{SD.AdminRole}")]
    public class CinemaController : Controller
    {

        ICenimaRepository CinemaRepository;
        public CinemaController(ICenimaRepository cinemaRepository)
        {
            CinemaRepository = cinemaRepository;
        }

        public IActionResult Index()
        {
            var cinemas = CinemaRepository.GetAll(new Func<IQueryable<Cinema>, IQueryable<Cinema>>[]
     {
                    q => q.Include(c => c.Movies)
                     .ThenInclude(p => p.Category)
     });
            return View(cinemas);
        }
        public IActionResult Detailes(int Id)
        {

            var moves = CinemaRepository.Getone(new Func<IQueryable<Cinema>, IQueryable<Cinema>>[]
            {
               q=>q.Include(p=>p.Movies)
               .ThenInclude(p=>p.Category)

            },
            filter: C => C.Id == Id
            );
            return View(moves);
        }
        public IActionResult Index2()
        {

            var cinemas = CinemaRepository.GetAll(new Func<IQueryable<Cinema>, IQueryable<Cinema>>[]

     {
                    
     });
            return View(cinemas);
        }
        public IActionResult Add()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Add(Cinema cinema)
        {
            CinemaRepository.Add(cinema);
            CinemaRepository.Commit();


            return RedirectToAction("Index2");
        }

        public IActionResult Edit(int Id)
        {
            var cinema= CinemaRepository.Getone(new Func<IQueryable<Cinema>, IQueryable<Cinema>>[]
            {

            },
            filter:e=>e.Id==Id
            
            
            );

            return View(cinema);
        }
        [HttpPost]
        public IActionResult Edit(Cinema cinema)
        {
            CinemaRepository.Edit
                (cinema);
            CinemaRepository.Commit();
            return RedirectToAction("Index2");

        }

     
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var category = CinemaRepository.Getone(new Func<IQueryable<Cinema>, IQueryable<Cinema>>[] { 
            
            }, 
            
            filter: e => e.Id == Id
            
            );

            if (category != null)
            {
                CinemaRepository.Delete(category);
                CinemaRepository.Commit();
            }

            return RedirectToAction("Index2");
        }



    }
}

