namespace MovieMaster.Models
{
    public class Review
    {
        public int ID { get; set; }
        public int User_ID { get; set; } 
        public int Movie_ID { get; set; }  
        public string ReviewText { get; set; } 
        public DateTime Time_date { get; set; } 
    }
}
