using Book_Reservation.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book_Reservation.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Login
        public IActionResult Login() => View();

        // POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserId", user.Id.ToString());

                TempData["Success"] = "Login successful!";
                return RedirectToAction("Dashboard", "Book");

            }

            TempData["Error"] = "Invalid login attempt.";
            return View(model);
        }

        // GET: /Account/SignUp
        public IActionResult SignUp() => View();

        // POST: /Account/SignUp
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (existingUser == null)
                {
                    var user = new User
                    {
                        Name = model.Name,
                        Email = model.Email,
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password)
                    };

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    TempData["Success"] = "Account created successfully!";
                    return RedirectToAction("Login");
                }
                TempData["Error"] = "Email already in use.";
            }
            return View(model);
        }

        // GET: /Account/ForgotPassword
        public IActionResult ForgotPassword() => View();
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        // POST: /Account/ForgotPassword
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user != null)
            {
                TempData["Success"] = "Reset link sent to your email!";
            }
            else
            {
                TempData["Error"] = "Email not found.";
            }
            return View(model);
        }


    }
}
