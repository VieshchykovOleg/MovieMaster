using MovieMaster.Models;

public class ActorMovie
{
    public int Actor_ID { get; set; } 
    public Actor Actor { get; set; }

    public int Movie_ID { get; set; } 
    public Movie Movie { get; set; }
}
