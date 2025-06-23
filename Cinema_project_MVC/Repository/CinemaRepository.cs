using Cinema_project_MVC.Data;
using Cinema_project_MVC.Models;
using Cinema_project_MVC.Repository.IReprsitory;
namespace Cinema_project_MVC.Repository
{
    public class CinemaRepository : Repository<Cinema>, ICenimaRepository
    {
        public CinemaRepository(AppDbContext db) : base(db)
        {
        }
    }
}
