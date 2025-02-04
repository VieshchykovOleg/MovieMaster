using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieMaster.Models
{
    public class Comment
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int Movie_ID { get; set; }

        [ForeignKey("Movie_ID")]
        public Movie Movie { get; set; }

        [Required]
        public int User_ID { get; set; }

        [ForeignKey("User_ID")]
        public User User { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        [Required]
        [Range(1, 10)]
        public int Rating { get; set; }

        [Column("Created_At")]  // Вказуємо правильне ім'я стовпця в базі даних
        public DateTime CreatedAt { get; set; }  // Використовуємо зручну назву для C#
    }
}
