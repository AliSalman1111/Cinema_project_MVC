using System.ComponentModel.DataAnnotations;

namespace Cinema_project_MVC.modelView
{
    public class loginVm
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
       
        public string passward { get; set; }
        public bool Remembermy { get; set; }

    }
}
