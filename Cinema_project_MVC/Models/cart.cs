using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema_project_MVC.Models
{
    public class cart
    {
        public int movieId { get; set; }
        [ForeignKey(nameof(movieId))]
        public  Movie movie { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }

        public int count { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }
    }
}
