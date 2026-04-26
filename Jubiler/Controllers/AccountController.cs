using Microsoft.AspNetCore.Mvc;
using Jubiler.Models;
using Jubiler.Data;
using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Jubiler.Controllers
{
    public class AccountController : Controller
    {
        private readonly JubilerContext _context;

        public AccountController(JubilerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string login, string firstName, string lastName, string email, string password)
        {
            if (_context.Users.Any(u => u.Login == login))
            {
                ModelState.AddModelError("", "Login already exists.");
                return View();
            }

            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var passwordHash = Convert.ToBase64String(hashedBytes);

            var newUser = new User
            {
                Login = login,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PasswordHash = passwordHash
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string loginOrEmail, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Login == loginOrEmail || u.Email == loginOrEmail);

            if (user != null)
            {
                using var sha256 = SHA256.Create();
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var inputHash = Convert.ToBase64String(hashedBytes);

                if (user.PasswordHash == inputHash)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Login),
                        new Claim("FullName", $"{user.FirstName} {user.LastName}")
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Products");
                }
            }

            ModelState.AddModelError("", "Invalid login or password.");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Products");
        }
    }
}
