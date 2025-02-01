namespace MovieMaster.Models
{
    public class Rating
    {
        public int ID { get; set; }
        public int Movie_ID { get; set; } 
        public int User_ID { get; set; }  
        public int RatingValue { get; set; }  
    }
}
