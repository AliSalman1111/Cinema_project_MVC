using Cinema_project_MVC.Data;
using Cinema_project_MVC.Models;
using Cinema_project_MVC.Repository;
using Cinema_project_MVC.Repository.IReprsitory;
using Cinema_project_MVC.StaticData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace Cinema_project_MVC.Controllers
{
    [Authorize(Roles = $"{SD.AdminRole},{SD.CompanyRole}")]
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

        public IActionResult Index2(int page, string? search)
        {

         
            var actor = actorRepository.GetAll(new Func<IQueryable<Actor>, IQueryable<Actor>>[]
{



});
            if (page <= 0)
            {
                page = 1;
            }
            int pageSize = 5;
            int totalProducts = actorRepository.GetAll().Count();

            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            if (search != null) {
                actor= actor.Where(e => e.FirstName.Contains(search)|| e.LastName.Contains(search));
            }

            actor= actor.Skip((page-1)*5).Take(5);
            return View(actor.ToList());
        }


        public IActionResult Add()
        {
  


            return View();
        }
        [HttpPost]
        public IActionResult Add(Actor actor,IFormFile ProfilePicture)
        {
            if (ProfilePicture.Length>0) {

                var fileName = Guid.NewGuid() + Path.GetExtension(ProfilePicture.FileName);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\cast", fileName);


                using (var stream = System.IO.File.Create(filePath))
                {
                    ProfilePicture.CopyTo(stream);
                }

                actor.ProfilePicture = fileName;
            }
            actorRepository.Add(actor);
            actorRepository.Commit();

            return RedirectToAction("Index2");



        }

        public IActionResult Edit(int Id)
        {

            var actor = actorRepository.Getone(new Func<IQueryable<Actor>, IQueryable<Actor>>[]
{



},
filter: e=>e.Id==Id
);

            return View(actor);
        }
        [HttpPost]
        public IActionResult Edit(Actor actor, IFormFile ProfilePicture)
        {
            var oldproduct = actorRepository.Getone(new Func<IQueryable<Actor>, IQueryable<Actor>>[]
           {
                     


           },
                  filter: c => c.Id == actor.Id
                  , tracked: false

           );
            if (ProfilePicture != null && ProfilePicture.Length > 0)
            {

                var fileName = Guid.NewGuid() + Path.GetExtension(ProfilePicture.FileName);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\cast", fileName);
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\cast", oldproduct.ProfilePicture);

                using (var stream = System.IO.File.Create(filePath))
                {
                    ProfilePicture.CopyTo(stream);
                }

                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);

                }
                actor.ProfilePicture = fileName;
            }
            else
            {

                actor.ProfilePicture = oldproduct.ProfilePicture;
            }
        
            actorRepository.Edit(actor);
            actorRepository.Commit();

            return RedirectToAction("Index2");



            
        }
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var actor = actorRepository.Getone(new Func<IQueryable<Actor>, IQueryable<Actor>>[]
               {
                    


            },
              filter: c => c.Id == Id

               );
            // Category category=new Category() { Name= CategoryName };
            var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\cast", actor.ProfilePicture);

            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);

            }
            if (actor != null)
            {
                actorRepository.Delete(actor);
                actorRepository.Commit();
            }
         
           
            return RedirectToAction("Index2");
        }
    }

    }

