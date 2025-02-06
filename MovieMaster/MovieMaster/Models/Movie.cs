using MovieMaster.Models;

public class Movie
{
    public int ID { get; set; }
    public string Title { get; set; }
    public int Release_Year { get; set; }
    public string Txt_Description { get; set; }

    public int Genre_ID { get; set; }  
    public Genre Genre { get; set; }  

    public ICollection<Comment> Comments { get; set; }
    public ICollection<ActorMovie> ActorsMovies { get; set; }
    public ICollection<DirectorMovie> DirectorsMovies { get; set; }
}
