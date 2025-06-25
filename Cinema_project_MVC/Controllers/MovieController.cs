using Microsoft.AspNetCore.Mvc;
using Cinema_project_MVC.Repository.IReprsitory;
using Microsoft.EntityFrameworkCore;
using Cinema_project_MVC.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cinema_project_MVC.Models;
namespace Cinema_project_MVC.Controllers
{
    public class MovieController : Controller
    {

        IMovieRepository repository;
        ICatrgoryRepository CategoryRepository;
        ICenimaRepository CenimaRepository;
        IMovieActor movieActor;
        IActorRepository actorRepository;
        public MovieController(IMovieRepository repository, ICatrgoryRepository categoryRepository, ICenimaRepository CenimaRepository, IMovieActor movieActor, IActorRepository actorRepository)
        {
            this.repository = repository;
            CategoryRepository = categoryRepository;
            this.CenimaRepository = CenimaRepository;
            this.movieActor = movieActor;
            this.actorRepository = actorRepository;
        }
        public IActionResult Index()
        {
            var moves = repository.GetAll(new Func<IQueryable<Models.Movie>, IQueryable<Models.Movie>>[]
            {
                 q=>q.Include(c=>c.Category).Include(c=>c.Cinema).Include(a=>a.ActorMovie).ThenInclude(a=>a.Actor)

            });
            return View(moves);
        }

        public IActionResult Add()
        {
            var categores = CategoryRepository.GetAll().Select(e => new SelectListItem { Text = e.Name, Value = e.Id.ToString() });
            ViewBag.categores = categores;
            var Cinemas = CenimaRepository.GetAll().Select(e => new SelectListItem { Text = e.Name, Value = e.Id.ToString() });
            ViewBag.Cinemas = Cinemas;
            var actors = actorRepository.GetAll(new Func<IQueryable<Actor>, IQueryable<Actor>>[] {

            }); // افترض أنك عندك Repo للممثلين

            ViewBag.Actors = actors;
            return View();
        }
        [HttpPost]
        public IActionResult Add(Movie movie, IFormFile Photo, List<int> SelectedActorIds)
        {

            if (Photo.Length > 0)
            {

                var fileName = Guid.NewGuid() + Path.GetExtension(Photo.FileName);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\movies", fileName);


                using (var stream = System.IO.File.Create(filePath))
                {
                    Photo.CopyTo(stream);
                }

                movie.Photo = fileName;
            }
            repository.Add(movie);
            repository.Commit();

            if (SelectedActorIds != null)
            {
                foreach (var actorId in SelectedActorIds)
                {
                    var actorMovie = new ActorMovie
                    {
                        MovieId = movie.Id,
                        ActorId = actorId
                    };
                    // _context.ActorMovies.Add(actorMovie);
                    movieActor.Add(actorMovie);
                }

                //_context.SaveChanges();

                movieActor.Commit();
            }

            return RedirectToAction("Index");
        }


        public IActionResult Edit(int Id)
        {
            var categores = CategoryRepository.GetAll().Select(e => new SelectListItem { Text = e.Name, Value = e.Id.ToString() });
            ViewBag.categores = categores;
            var Cinemas = CenimaRepository.GetAll().Select(e => new SelectListItem { Text = e.Name, Value = e.Id.ToString() });
            ViewBag.Cinemas = Cinemas;
            var actors = actorRepository.GetAll(new Func<IQueryable<Actor>, IQueryable<Actor>>[] {

        });
            ViewBag.Actors = actors;
            var selectedActorIds = movieActor
                 .GetAll(new Func<IQueryable<ActorMovie>, IQueryable<ActorMovie>>[]
                 {


                 },
                
                filter: am => am.MovieId == Id)
                .Select(am => am.ActorId).ToList();



            ViewBag.SelectedActorIds = selectedActorIds;
            var movie = repository.Getone(new Func<IQueryable<Movie>, IQueryable<Movie>>[]
            {


            },
             filter: e=>e.Id==Id
            );
            return View(movie);
        }
        [HttpPost]
        public IActionResult Edit(Movie movie, IFormFile Photo, List<int> SelectedActorIds)
        {

            var oldproduct = repository.Getone(new Func<IQueryable<Movie>, IQueryable<Movie>>[]
                      {



                      },
                             filter: c => c.Id == movie.Id
                             , tracked: false

                      );
            if (Photo != null && Photo.Length > 0)
            {

                var fileName = Guid.NewGuid() + Path.GetExtension(Photo.FileName);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\movies", fileName);
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\movies", oldproduct.Photo);

                using (var stream = System.IO.File.Create(filePath))
                {
                    Photo.CopyTo(stream);
                }

                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);

                }
                movie.Photo = fileName;
            }
            else
            {

                movie.Photo = oldproduct.Photo;
            }

            repository.Edit(movie);
            repository.Commit();



            var oldActorMovies = movieActor.GetAll(
          filter: am => am.MovieId == movie.Id
               ).ToList();
            foreach (var am in oldActorMovies)
            {
                movieActor.Delete(am); // استخدم الريبو لو عندك
            }
            repository.Commit();

            // 3. إضافة الممثلين الجدد اللي تم اختيارهم
            if (SelectedActorIds != null && SelectedActorIds.Any())
            {
                foreach (var actorId in SelectedActorIds)
                {
                    var newRelation = new ActorMovie
                    {
                        MovieId = movie.Id,
                        ActorId = actorId
                    };
                    movieActor.Add(newRelation);
                }
                repository.Commit();
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var actor = repository.Getone(new Func<IQueryable<Movie>, IQueryable<Movie>>[]
               {



            },
              filter: c => c.Id == Id

               );
            // Category category=new Category() { Name= CategoryName };
            var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\movies", actor.Photo);

            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);

            }
            if (actor != null)
            {
                repository.Delete(actor);
                repository.Commit();
            }


            return RedirectToAction("Index");
        }


    }
    }


