using Cinema_project_MVC.Models;
using Cinema_project_MVC.Repository;
using Cinema_project_MVC.Repository.IReprsitory;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema_project_MVC.Controllers
{
    public class CartController : Controller
    {
        


        UserManager<ApplicationUser> userManager;
       ICartRepository CartRepositorty;
        public CartController(UserManager<ApplicationUser> userManager, ICartRepository CartRepositorty)
        {
            this.userManager = userManager;
            this.CartRepositorty = CartRepositorty;
        }

        public IActionResult AddToCart(int count ,int movieId, DateTime dateTime)
        {


            cart cart = new cart() { 
            
            movieId= movieId,
            count = count,
                DateTime = dateTime,

              ApplicationUserId= userManager.GetUserId(User)

            };

            CartRepositorty.Add(cart);
            CartRepositorty.Commit();
            int x;

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
        {

            var userid = userManager.GetUserId(User);



            var carts = CartRepositorty.GetAll(new Func<IQueryable<cart>, IQueryable<cart>>[]
            {
            q=>q.Include(x=>x.movie).Where(e=>e.ApplicationUserId == userid)
            });

            ViewBag.products = carts.Sum(x => x.movie.Price * x.count);
            return View(carts.ToList());

        }

        public IActionResult Increment(int Id)
        {

            var userid = userManager.GetUserId(User);
            var product = CartRepositorty.Getone(new Func<IQueryable<cart>, IQueryable<cart>>[]
            {

            }, filter: e => e.ApplicationUserId == userid && e.movieId == Id


            );

            if (product != null)
            {
                product.count++;
                CartRepositorty.Commit();
                return RedirectToAction("Index");

            }

            return RedirectToAction("NotFountPage", "Home");
        }



        public IActionResult Decress(int Id)
        {

            var userid = userManager.GetUserId(User);
            var product = CartRepositorty.Getone(new Func<IQueryable<cart>, IQueryable<cart>>[]
            {

            }, filter: e => e.ApplicationUserId == userid && e.movieId == Id


            );
            if (product != null)
            {
                product.count--;

                if (product.count > 0)

                    CartRepositorty.Commit();
                else
                    product.count = 1;

                return RedirectToAction("Index");

            }

            return RedirectToAction("NotFountPage", "Home");
        }



        public IActionResult Delete(int Id)
        {

            var userid = userManager.GetUserId(User);
            var product = CartRepositorty.Getone(new Func<IQueryable<cart>, IQueryable<cart>>[]
            {

            }, filter: e => e.ApplicationUserId == userid && e.movieId == Id


            );
            if (product != null)
            {
                CartRepositorty.Delete(product);
                CartRepositorty.Commit();
                return RedirectToAction("Index");

            }

            return RedirectToAction("NotFountPage", "Home");
        }
    }
}
