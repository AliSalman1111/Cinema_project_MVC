namespace Cinema_project_MVC.Models
{
    public class ActorMovie
    {
        public int MovieId { get; set; }
        public int ActorId { get; set; }

        public Actor Actor { get; set; }

        public Movie Movie { get; set; }

    }
}
