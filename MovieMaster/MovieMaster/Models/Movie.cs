namespace MovieMaster.Models
{
    public class Movie
    {
        public int ID { get; set; }
        public string Title { get; set; } 
        public int Release_Year { get; set; }  
        public string Genre { get; set; } 
        public string Txt_Description { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
