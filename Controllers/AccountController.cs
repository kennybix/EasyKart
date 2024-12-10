//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using OnlineShopping.Data;
//using OnlineShopping.Models;

//namespace OnlineShopping.Controllers
//{
//    public class AccountController : BaseController
//    {

//        private readonly OnlineShoppingDbContext _context;

//        public AccountController(OnlineShoppingDbContext context)
//        {
//            _context = context;
//        }

//        public IActionResult Login()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Login(string username, string password)
//        {
//            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
//            if (user != null)
//            {
//                HttpContext.Session.SetInt32("UserId", user.Id);
//                HttpContext.Session.SetString("Username", user.Username);
//                return RedirectToAction("Index", "Product");
//            }
//            ModelState.AddModelError("", "Invalid username or password");
//            return View();
//        }

//        public IActionResult Register()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Register(User user)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Users.Add(user);
//                _context.SaveChanges();
//                return RedirectToAction("Login");
//            }
//            return View(user);
//        }

//        public IActionResult Logout()
//        {
//            HttpContext.Session.Clear();
//            return RedirectToAction("Login");
//        }
//    }
//}


using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Data;
using OnlineShopping.Models;
using System.Linq;

namespace OnlineShopping.Controllers
{
    public class AccountController : BaseController
    {
        private readonly OnlineShoppingDbContext _context;

        public AccountController(OnlineShoppingDbContext context)
        {
            _context = context;
        }

        // Login Page
        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                return RedirectToAction("Profile", "User");
            }
            return View();
        }

        // Handle Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("", "Username and password are required.");
                return View();
            }

            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                // Set session variables
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("Username", user.Username);

                TempData["Message"] = "Logged in successfully!";
                return RedirectToAction("Profile", "User");
            }

            ModelState.AddModelError("", "Invalid username or password.");
            return View();
        }

        // Registration Page
        public IActionResult Register()
        {
            return View();
        }

        // Handle Registration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Username == user.Username))
                {
                    ModelState.AddModelError("Username", "This username is already taken.");
                    return View(user);
                }

                if (_context.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "This email is already registered.");
                    return View(user);
                }

                // Add user to the database
                _context.Users.Add(user);
                _context.SaveChanges();

                TempData["Message"] = "Registration successful! Please log in.";
                return RedirectToAction("Login");
            }
            return View(user);
        }

        // Handle Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Message"] = "Logged out successfully!";
            return RedirectToAction("Login");
        }
    }
}

