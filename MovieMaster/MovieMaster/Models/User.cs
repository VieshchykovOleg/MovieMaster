using System.ComponentModel.DataAnnotations;

namespace MovieMaster.Models
{
    public class User
    {
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name_User { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Range(5, 120)]
        public int Age { get; set; }

        [Required]
        public string Occupation { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 6)] 
        public string User_Password { get; set; }
        public bool IsAdmin { get; set; } = false;
    }
}
