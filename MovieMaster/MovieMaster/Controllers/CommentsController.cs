using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieMaster.Data;
using MovieMaster.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MovieMaster.Controllers
{
    public class CommentsController : Controller
    {
        private readonly MovieDbContext _context;

        public CommentsController(MovieDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(int movieId, string content, int rating)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Users");  
            }
           
          
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Register", "Users");  
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
            {
                return RedirectToAction("Register", "Users");
            }

            var userId = user.ID;

            var comment = new Comment
            {
                Movie_ID = movieId,
                User_ID = userId,  
                Content = content,
                Rating = rating,
                CreatedAt = DateTime.Now 
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            var movie = _context.Movies.FirstOrDefault(m => m.ID == movieId);
            if (movie != null)
            {
                var averageRating = movie.Comments.Any() ? movie.Comments.Average(c => c.Rating) / 10 : 0;
                ViewBag.AverageRating = averageRating;
            }
            return RedirectToAction("Details", "Movies", new { id = movieId });
        }
        public async Task<IActionResult> GetComments(int movieId)
        {
            var comments = await _context.Comments
                .Where(c => c.Movie_ID == movieId)
                .Include(c => c.User)
                .ToListAsync();

            return PartialView("_CommentsPartial", comments);
        }
    }
}
