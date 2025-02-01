using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using MovieMaster.Models;

namespace MovieMaster.Data
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
   

        // Метод для перевірки з'єднання з базою даних
        public bool CanConnect()
        {
            return Database.CanConnect();
        }
    }
}
