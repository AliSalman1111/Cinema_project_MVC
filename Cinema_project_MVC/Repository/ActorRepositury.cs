namespace Cinema_project_MVC.Repository;

using Cinema_project_MVC.Data;
using Cinema_project_MVC.Models;
using Cinema_project_MVC.Repository.IReprsitory;

public class ActorRepositury : Repository<Actor>, IActorRepository
{
    public ActorRepositury(AppDbContext db) : base(db)
    {
    }
}
