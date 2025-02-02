using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieMaster.Data;
using MovieMaster.Models;

namespace MovieMaster.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieDbContext _context;

        public MoviesController(MovieDbContext context)
        {
            _context = context;
        }
       
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movies.ToListAsync());
        }
    }
}
