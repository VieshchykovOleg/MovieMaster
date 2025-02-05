using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Director
{
    [Key]
    [Column("ID")]
    public int ID { get; set; }

    public string Name_Director { get; set; }
    public DateTime Birthdate { get; set; }
    public string Country { get; set; }

    public ICollection<DirectorMovie> DirectorsMovies { get; set; }
}
