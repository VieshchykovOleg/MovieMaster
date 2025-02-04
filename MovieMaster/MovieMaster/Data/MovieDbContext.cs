using Microsoft.EntityFrameworkCore;
using MovieMaster.Models;

namespace MovieMaster.Data
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Actors> Actors { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<ActorMovie> ActorsMovies { get; set; }
        public DbSet<DirectorMovie> DirectorsMovies { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          
            modelBuilder.Entity<ActorMovie>()
                .HasKey(am => new { am.Actor_ID, am.Movie_ID });
            modelBuilder.Entity<DirectorMovie>()
                .HasKey(dm => new { dm.Director_ID, dm.Movie_ID });
        }
    }
}
