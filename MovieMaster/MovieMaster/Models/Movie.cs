namespace MovieMaster.Models
{
    public class Movie
    {
        public int ID { get; set; }
        public string Title { get; set; } 
        public int Release_Year { get; set; }  

        public string Txt_Description { get; set; }

        // Зовнішній ключ для жанру
        // Зовнішній ключ для жанру
        public int Genre_ID { get; set; }

        // Навігаційна властивість для жанру (можна змінити ім'я на GenreInfo або інше)
        public Genre GenreInfo { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<ActorMovie> ActorsMovies { get; set; }
        public ICollection<DirectorMovie> DirectorsMovies { get; set; }
     
    }
}
