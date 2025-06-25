using Cinema_project_MVC.Data;
using Cinema_project_MVC.Models;
using Cinema_project_MVC.Repository.IReprsitory;

namespace Cinema_project_MVC.Repository
{
    public class MonieActor : Repository<ActorMovie>, IMovieActor
    {
        public MonieActor(AppDbContext db) : base(db)
        {
        }
    }
}
