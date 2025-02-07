using Microsoft.AspNetCore.Mvc;
using MovieMaster.Data;
using MovieMaster.Models;
using System.Linq;

namespace MovieMaster.Controllers
{
    public class AdminController : Controller
    {
        private readonly MovieDbContext _context;

        public AdminController(MovieDbContext context)
        {
            _context = context;
        }

        private bool IsAdmin()
        {
            return User.Identity.IsAuthenticated &&
                   bool.TryParse(User.Claims.FirstOrDefault(c => c.Type == "IsAdmin")?.Value, out bool isAdmin) &&
                   isAdmin;
        }

        public IActionResult Index()
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Movies");
            var movies = _context.Movies.ToList();
            return View(movies);
        }

        public IActionResult Create()
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Movies");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            
                if (!ModelState.IsValid)
                {
                    
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                    }

                    return View(movie);
                }

                if (!IsAdmin())
                {
                    return RedirectToAction("Index", "Movies");
                }
                _context.Movies.Add(movie);
                _context.SaveChanges();

                return RedirectToAction("Index");

    }

        public IActionResult Edit(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Movies");
            var movie = _context.Movies.Find(id);
            if (movie == null) return NotFound();
            return View(movie);
        }

        [HttpPost]
        public IActionResult Edit(int id, Movie movie)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Movies");
            if (ModelState.IsValid)
            {
                _context.Movies.Update(movie);
                _context.SaveChanges();
                return RedirectToAction("Index", "Movies");
            }
            return View(movie);
        }

        public IActionResult Delete(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Movies");
            var movie = _context.Movies.Find(id);
            if (movie == null) return NotFound();
            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }
    }
}