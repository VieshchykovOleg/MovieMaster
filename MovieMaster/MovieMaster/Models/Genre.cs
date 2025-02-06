namespace MovieMaster.Models
{
    public class Genre
    {
        public int ID { get; set; }
        public string Genre_Name { get; set; }

        // Колекція для зв'язку з Movie
        public ICollection<Movie> Movies { get; set; }
    }

}
