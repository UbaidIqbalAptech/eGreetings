using System.Diagnostics;
using eGreetings.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace eGreetings.Controllers
{
    public class HomeController : Controller
    {
		private readonly EGreetingsContext _context;
        private readonly ILogger<HomeController> _logger;

		public HomeController(EGreetingsContext context, ILogger<HomeController> logger)
        {
			_context = context;
			_logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Partial()
        {
            return View();
        }

		public IActionResult MainView()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Register(User user)
		{
			if (ModelState.IsValid)
			{
				_context.Users.Add(user);
				_context.SaveChanges();

				TempData["AlertMessage"] = "User data saved successfully!";
			}
			else
			{
				TempData["AlertMessage"] = "Failed to save user data. Please check your input and try again.";
			}

			return RedirectToAction("MainView");
		}


        public IActionResult ViewUsersPartial()
        {
            var user = _context.Users.ToList();
            return PartialView("_ViewUsersPartial", user);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
		public JsonResult GetUserById(int id)
		{
			using (var db = new EGreetingsContext())
			{
				var user = db.Users.FirstOrDefault(u => u.UserId == id);
				return Json(user);
			}
		}

		[HttpPost]
		public JsonResult DeleteUser(int id)
		{
			using (var db = new EGreetingsContext())
			{
				var user = db.Users.FirstOrDefault(u => u.UserId == id);
				if (user != null)
				{
					try
					{
						db.Entry(user).Reload(); 
						db.Users.Remove(user);
						db.SaveChanges();
						return Json(new { success = true });
					}
					catch (DbUpdateConcurrencyException)
					{
						return Json(new { success = false, message = "Concurrency conflict occurred. Please try again." });
					}
				}
				else
				{
					return Json(new { success = false, message = "User not found." });
				}
			}
		}


		[HttpPost]
		public IActionResult UpdateUser([FromBody] User updatedUser)
		{
			if (ModelState.IsValid)
			{
				var user = _context.Users.Find(updatedUser.UserId);
				if (user != null)
				{
					user.Username = updatedUser.Username;
					user.Email = updatedUser.Email;
					user.Password = updatedUser.Password;

					
					_context.SaveChanges();
					return Ok();
				}
				return NotFound();
			}

			foreach (var state in ModelState)
			{
				Console.WriteLine($"Key: {state.Key}, Errors: {string.Join(", ", state.Value.Errors.Select(e => e.ErrorMessage))}");
			}
			return BadRequest(ModelState);
		}

		//Register login
		[HttpGet]
        public IActionResult SignUp()
		{
			return View();
		}
		
        //get login
        public IActionResult SignIn()
		{
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> SignUp(User user)
        {
            if (ModelState.IsValid)
            {
                if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                {

					return RedirectToAction("SignUp", "Home");
				}

               
                user.RegistrationDate = DateTime.Now;
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                await SignInUser(user);//
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("SignUp", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                await SignInUser(user);
                return RedirectToAction("Index", "Home");
            }


            return RedirectToAction("Index", "Home");
        }

        private async Task SignInUser(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()) // Add User ID claim
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }









    }
}