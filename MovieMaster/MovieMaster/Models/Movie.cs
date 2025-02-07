namespace MovieMaster.Models
{
    public class Movie
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int Release_Year { get; set; }
        public string Txt_Description { get; set; }
        public string Genre { get; set; } 

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<ActorMovie> ActorsMovies { get; set; } = new List<ActorMovie>();
        public ICollection<DirectorMovie> DirectorsMovies { get; set; } = new List<DirectorMovie>();
    }
}