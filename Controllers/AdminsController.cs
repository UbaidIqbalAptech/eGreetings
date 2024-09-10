using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using eGreetings.Models;

namespace eGreetings.Controllers
{
    public class AdminController : Controller
    {
        private readonly EGreetingsContext _context;

        public AdminController(EGreetingsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("Email", "Password")] User login)
        {
            if (ModelState.IsValid)
            {
                if (login.Email == "admin@gmail.com" && login.Password == "admin123")
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Role, "Admin"),
                    };

                    var identity = new ClaimsIdentity(claims, "Login");
                    var principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(principal);
                    return RedirectToAction("Index", "Admins");
                }

                var user = _context.Users.FirstOrDefault(u => u.Email == login.Email && u.Password == login.Password);
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                        new Claim(ClaimTypes.Role, "User"),
                    };

                    var identity = new ClaimsIdentity(claims, "Login");
                    var principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(principal);

                    return RedirectToAction("MainView", "Home"); // Redirect to another action after successful login
                }
                else
                {
                    TempData["LoginError"] = "Invalid username or password.";
                    return View();
                }
            }
            TempData["LoginError"] = "Invalid username or password.";
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}


