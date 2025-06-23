namespace Cinema_project_MVC.Repository;

using Cinema_project_MVC.Data;
using Cinema_project_MVC.Models;
using Cinema_project_MVC.Repository.IReprsitory;


    public class CategotyRepository : Repository<Category>, ICatrgoryRepository
{
    public CategotyRepository(AppDbContext db) : base(db)
    {
    }
}

