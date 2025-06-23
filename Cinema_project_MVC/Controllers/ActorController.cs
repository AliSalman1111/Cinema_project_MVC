using Cinema_project_MVC.Data;
using Cinema_project_MVC.Models;
using Cinema_project_MVC.Repository.IReprsitory;
using Microsoft.AspNetCore.Mvc;

namespace Cinema_project_MVC.Controllers
{
    public class ActorController : Controller
    {

        IActorRepository   actorRepository;

        public ActorController(IActorRepository actorRepository)
        {
            this.actorRepository = actorRepository;
        }

        public IActionResult Index(int Id)
        {

            var actor = actorRepository.Getone(new Func<IQueryable<Actor>, IQueryable<Actor>>[]
            {


            },
            filter:e=>e.Id==Id


                );
            return View(actor);
        }

    }
}
