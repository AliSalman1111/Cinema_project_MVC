using Cinema_project_MVC.Data;
using Cinema_project_MVC.Models;
using Cinema_project_MVC.Repository.IReprsitory;

namespace Cinema_project_MVC.Repository
{
    public class CartRepositorty : Repository<cart>, ICartRepository
    {
        public CartRepositorty(AppDbContext db) : base(db)
        {

        }
    }
}
