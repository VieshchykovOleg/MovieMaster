using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Actor
{
    [Key]
    [Column("ID")] 
    public int ID { get; set; } 

    public string Name_Actor { get; set; }
    public DateTime Birthdate { get; set; }
    public string Gender { get; set; }
    public string Country { get; set; }
    public ICollection<ActorMovie> ActorsMovies { get; set; }
}
