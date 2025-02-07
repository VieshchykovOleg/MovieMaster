using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieMaster.Data;
using MovieMaster.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMaster.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieDbContext _context;

        public MoviesController(MovieDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string title, int? releaseYear, int? rating, string actor,string director, string genre, bool? sortByRating)
        {
            var moviesQuery = _context.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                moviesQuery = moviesQuery.Where(m => m.Title.Contains(title));
            }
            if (releaseYear.HasValue)
            {
                moviesQuery = moviesQuery.Where(m => m.Release_Year == releaseYear);
            }

            if (rating.HasValue)
            {
                moviesQuery = moviesQuery.Where(m => m.Comments.Any(c => c.Rating == rating.Value));
            }

            if (!string.IsNullOrEmpty(actor))
            {
                moviesQuery = moviesQuery.Where(m => m.ActorsMovies
                                                       .Any(am => am.Actor.Name_Actor.Contains(actor)));
            }
            if (!string.IsNullOrEmpty(director))
            {
                moviesQuery = moviesQuery.Where(m => m.DirectorsMovies
                                                       .Any(dm => dm.Director.Name_Director.Contains(director)));
            }

            if (!string.IsNullOrEmpty(genre))
            {
                moviesQuery = moviesQuery.Where(m => m.Genre.Contains(genre));
            }

            var movies = await moviesQuery
                .Include(m => m.Comments)
                .Include(m => m.ActorsMovies)
                .ThenInclude(am => am.Actor)
                .ToListAsync();

            if (sortByRating.HasValue && sortByRating.Value)
            {
                movies = movies.OrderBy(m => m.Comments.Any() ? m.Comments.Average(c => c.Rating) : 0).ToList();
            }

            return View(movies);
        }

        public async Task<IActionResult> Details(int id)
        {
            var movie = await _context.Movies
                .Include(m => m.Comments)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (movie == null)
            {
                return NotFound();
            }

            var averageRating = movie.Comments.Any() ? movie.Comments.Average(c => c.Rating) / 10 : 0;
            ViewBag.AverageRating = averageRating;

            return View(movie);
        }
    }
}
