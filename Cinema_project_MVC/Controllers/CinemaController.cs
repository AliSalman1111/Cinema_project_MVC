using Cinema_project_MVC.Data;
using Cinema_project_MVC.Models;
using Cinema_project_MVC.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema_project_MVC.Controllers
{
    public class CinemaController : Controller
    {

        CinemaRepository CinemaRepository;
        public CinemaController(CinemaRepository cinemaRepository)
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
    }
}
