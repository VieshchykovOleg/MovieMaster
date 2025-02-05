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
        public async Task<IActionResult> Index(string title, int? releaseYear, int? rating, string actor, string director)
        {
            var moviesQuery = _context.Movies.AsQueryable();

            // Фільтрація за назвою
            if (!string.IsNullOrEmpty(title))
            {
                moviesQuery = moviesQuery.Where(m => m.Title.Contains(title));
            }

            // Фільтрація за роком випуску
            if (releaseYear.HasValue)
            {
                moviesQuery = moviesQuery.Where(m => m.Release_Year == releaseYear);
            }

            // Фільтрація за рейтингом
            if (rating.HasValue)
            {
                moviesQuery = moviesQuery.Where(m => m.Comments.Any(c => c.Rating == rating.Value));
            }

            // Фільтрація за актором
            if (!string.IsNullOrEmpty(actor))
            {
                moviesQuery = moviesQuery.Where(m => m.ActorsMovies
                                                       .Any(am => am.Actor.Name_Actor.Contains(actor)));
            }

            // Фільтрація за режисером
            if (!string.IsNullOrEmpty(director))
            {
                moviesQuery = moviesQuery.Where(m => m.DirectorsMovies
                                                       .Any(dm => dm.Director.Name_Director.Contains(director)));
            }

            var movies = await moviesQuery
                .Include(m => m.Comments)
                .Include(m => m.ActorsMovies)
                .ThenInclude(am => am.Actor)
                .Include(m => m.DirectorsMovies)  
                .ThenInclude(dm => dm.Director)  
                .ToListAsync();

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
