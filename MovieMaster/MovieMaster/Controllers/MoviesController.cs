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
        public async Task<IActionResult> Index(string title, int? year, int? rating, string genre)
        {
            var moviesQuery = _context.Movies
                .Include(m => m.Comments)
                .AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                moviesQuery = moviesQuery.Where(m => m.Title.Contains(title));
            }

            if (year.HasValue)
            {
                moviesQuery = moviesQuery.Where(m => m.Release_Year == year);
            }

            if (!string.IsNullOrEmpty(genre))
            {
                moviesQuery = moviesQuery.Where(m => m.Genre.Contains(genre));
            }

            if (rating.HasValue)
            {
                moviesQuery = moviesQuery.Where(m => m.Comments.Any() &&
                                                     Math.Round(m.Comments.Average(c => c.Rating)) == rating.Value);
            }

            var movies = await moviesQuery
                .ToListAsync();

            movies = movies
                .OrderByDescending(m => m.Comments.Any() ? m.Comments.Average(c => c.Rating) : 0)
                .ToList();

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
