using Cinema_project_MVC.Models;
using Cinema_project_MVC.modelView;
using Cinema_project_MVC.StaticData;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace Cinema_project_MVC.Controllers
{
    public class AccountController : Controller
    {
        UserManager<ApplicationUser> userManager;
        SignInManager<ApplicationUser> signInManager;
        RoleManager<IdentityRole> RoleManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> RoleManager) { 
        
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.RoleManager = RoleManager;
        }
        public async Task<IActionResult> RegisterAsync()
        {
            if (RoleManager.Roles.IsNullOrEmpty())
            {

                await RoleManager.CreateAsync(new(SD.AdminRole));
                await RoleManager.CreateAsync(new(SD.CompanyRole));
                await RoleManager.CreateAsync(new(SD.CustomerRole));
            }

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(ApplicationUserVM applicationUserVM)
        {
            if (ModelState.IsValid) { 
            
            ApplicationUser applicationUser=new ApplicationUser()
            {
                UserName = applicationUserVM.Name,
                Email = applicationUserVM.Email,
                address = applicationUserVM.Address,



            };

                var result= await userManager.CreateAsync(applicationUser, applicationUserVM.Password);

                if (result.Succeeded) {

                    await userManager.AddToRoleAsync(applicationUser, SD.CustomerRole);
                    await signInManager.SignInAsync(applicationUser, false);
                    return RedirectToAction("Index", "Home");
                
                }

                else
                {
                    ModelState.AddModelError("Password", "error password");
                }

            }

            return View(applicationUserVM);
        }

        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(loginVm loginVm)
        {
            if (ModelState.IsValid) { 
            
            var user = await userManager.FindByNameAsync(loginVm.UserName);

                if (user != null)
                {

                    var result = await userManager.CheckPasswordAsync(user, loginVm.passward);


                    if (result)
                    {
                        await signInManager.SignInAsync(user, loginVm.Remembermy);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("passward", "Invalid passward");
                    }
                }
                else
                {
                    ModelState.AddModelError("UserName", "error UserName");
                }
            
            }
            return View();
        }
        public IActionResult Logout()
        {
            signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
