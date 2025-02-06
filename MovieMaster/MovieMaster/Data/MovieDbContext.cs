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
        public DbSet<Genre> Genres { get; set; } 
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<ActorMovie> ActorsMovies { get; set; }
        public DbSet<DirectorMovie> DirectorsMovies { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DirectorMovie>()
                .HasKey(dm => new { dm.Director_ID, dm.Movie_ID });

            modelBuilder.Entity<DirectorMovie>()
                .HasOne(dm => dm.Director)
                .WithMany(d => d.DirectorsMovies)
                .HasForeignKey(dm => dm.Director_ID);

            modelBuilder.Entity<DirectorMovie>()
                .HasOne(dm => dm.Movie)
                .WithMany(m => m.DirectorsMovies)
                .HasForeignKey(dm => dm.Movie_ID);

            modelBuilder.Entity<Actor>()
                .HasKey(a => a.ID);

            modelBuilder.Entity<ActorMovie>()
                .HasKey(am => new { am.Actor_ID, am.Movie_ID });

            modelBuilder.Entity<ActorMovie>()
                .HasOne(am => am.Actor)
                .WithMany(a => a.ActorsMovies)
                .HasForeignKey(am => am.Actor_ID);

            modelBuilder.Entity<ActorMovie>()
                .HasOne(am => am.Movie)
                .WithMany(m => m.ActorsMovies)
                .HasForeignKey(am => am.Movie_ID);

            modelBuilder.Entity<Movie>()
                .HasOne(m => m.GenreInfo)
                .WithMany(g => g.Movies)
                .HasForeignKey(m => m.Genre_ID);

            
        }
    }
}
