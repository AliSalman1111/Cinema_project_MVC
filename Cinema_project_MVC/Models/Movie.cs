namespace Cinema_project_MVC.Models
{

    public enum MoveStatus
    {
       Upcoming,
            Avilable, 
            Expired
    }
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Photo { get; set; }
        public string TrailerUrl { get; set; }
        public MoveStatus moveStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int CinemaId { get; set; }
        public int CategoryId { get; set; }
        public Cinema Cinema { get; set; }
        public Category Category { get; set; }
        public List<ActorMovie> ActorMovie { get; set; }



    }
}
