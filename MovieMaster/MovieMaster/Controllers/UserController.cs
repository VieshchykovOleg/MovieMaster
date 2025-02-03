using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MovieMaster.Models;
using MovieMaster.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MovieMaster.Controllers
{
    public class UsersController : Controller
    {
        private readonly MovieDbContext _context;

        public UsersController(MovieDbContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View(new User());
        }


        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Перевірка чи існує користувач з таким же Email
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == user.Email);

                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Email already in use.");
                    return View(user);
                }

               
                user.IsAdmin = false;
                var passwordHasher = new PasswordHasher<User>();
                user.User_Password = passwordHasher.HashPassword(user, user.User_Password);

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login");
            }
            return View(user);
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
          
            if (string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("User_Password", "Password cannot be empty.");
                return View();
            }

        
            var user = await _context.Users
                                      .FirstOrDefaultAsync(u => u.Email == email);

         
            if (user != null)
            {
                var passwordHasher = new PasswordHasher<User>();
                var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.User_Password, password);

                if (passwordVerificationResult == PasswordVerificationResult.Success)
                {
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name_User),  
                new Claim(ClaimTypes.Email, user.Email),     
                new Claim("IsAdmin", user.IsAdmin.ToString()) 
            };
                  
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    return RedirectToAction("Index", "Movies");
                }
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }



        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Movies");
        }
    }
}
