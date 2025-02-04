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

        // Відображення списку фільмів
        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movies
                .Include(m => m.Comments) // Завантажуємо коментарі
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
