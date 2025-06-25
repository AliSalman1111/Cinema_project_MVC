using Microsoft.AspNetCore.Identity;

namespace Cinema_project_MVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string address { get; set; }
    }
}
