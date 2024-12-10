//using Microsoft.AspNetCore.Mvc;
//using OnlineShopping.Data;
//using OnlineShopping.Models;

//namespace OnlineShopping.Controllers
//{
//    public class UserController : BaseController
//    {

//        private readonly OnlineShoppingDbContext _context;

//        public UserController(OnlineShoppingDbContext context)
//        {
//            _context = context;
//        }
//        [HttpGet]
//        public IActionResult Profile()
//        {
//            var userId = HttpContext.Session.GetInt32("UserId");
//            if (userId == null)
//            {
//                return RedirectToAction("Login", "Account");
//            }

//            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
//            return View(user);
//        }

//        [HttpPost]
//        public IActionResult Profile(User updatedUser)
//        {
//            var userId = HttpContext.Session.GetInt32("UserId");
//            if (userId == null)
//            {
//                return RedirectToAction("Login", "Account");
//            }

//            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
//            if (user != null)
//            {
//                user.Address = updatedUser.Address;
//                user.PhoneNumber = updatedUser.PhoneNumber;
//                _context.SaveChanges();
//            }

//            return RedirectToAction("Profile");
//        }


//        [HttpGet]
//        public IActionResult Edit()
//        {
//            var username = User.Identity.Name; // Assumes the username is stored in the identity
//            var user = _context.Users.FirstOrDefault(u => u.Username == username);

//            if (user == null)
//            {
//                return NotFound();
//            }

//            return View(user);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Edit(User updatedUser)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(updatedUser);
//            }

//            var username = User.Identity.Name; // Assumes the username is stored in the identity
//            var user = _context.Users.FirstOrDefault(u => u.Username == username);

//            if (user == null)
//            {
//                return NotFound();
//            }

//            user.Email = updatedUser.Email;
//            user.Address = updatedUser.Address;
//            user.PhoneNumber = updatedUser.PhoneNumber;

//            _context.SaveChanges();

//            TempData["Message"] = "Profile updated successfully!";
//            return RedirectToAction("Profile");
//        }


//        //public IActionResult Index()
//        //{
//        //    return View();
//        //}
//    }
//}

//using Microsoft.AspNetCore.Mvc;
//using OnlineShopping.Data;
//using OnlineShopping.Models;
//using System.Linq;

//namespace OnlineShopping.Controllers
//{
//    public class UserController : BaseController
//    {
//        private readonly OnlineShoppingDbContext _context;

//        public UserController(OnlineShoppingDbContext context)
//        {
//            _context = context;
//        }

//        // Profile Page
//        [HttpGet]
//        public IActionResult Profile()
//        {
//            var userId = HttpContext.Session.GetInt32("UserId");
//            if (userId == null)
//            {
//                return RedirectToAction("Login", "Account");
//            }

//            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
//            if (user == null)
//            {
//                return NotFound("User not found.");
//            }

//            return View(user);
//        }

//        // Edit Profile GET
//        [HttpGet]
//        public IActionResult Edit()
//        {
//            var userId = HttpContext.Session.GetInt32("UserId");
//            if (userId == null)
//            {
//                return RedirectToAction("Login", "Account");
//            }

//            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
//            if (user == null)
//            {
//                return NotFound("User not found.");
//            }

//            return View(user);
//        }

//        // Edit Profile POST
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Edit(User updatedUser)
//        {
//            var userId = HttpContext.Session.GetInt32("UserId");
//            if (userId == null)
//            {
//                return RedirectToAction("Login", "Account");
//            }

//            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
//            if (user == null)
//            {
//                return NotFound("User not found.");
//            }

//            if (!ModelState.IsValid)
//            {
//                return View(updatedUser);
//            }

//            // Update user details
//            user.Email = updatedUser.Email;
//            user.Address = updatedUser.Address;
//            user.PhoneNumber = updatedUser.PhoneNumber;

//            _context.SaveChanges();

//            TempData["Message"] = "Profile updated successfully!";
//            return RedirectToAction("Profile");
//        }
//    }
//}


using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Data;
using OnlineShopping.Models;
using System.Linq;

namespace OnlineShopping.Controllers
{
    public class UserController : BaseController
    {
        private readonly OnlineShoppingDbContext _context;

        public UserController(OnlineShoppingDbContext context)
        {
            _context = context;
        }

        // Profile Page
        [HttpGet]
        public IActionResult Profile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(user);
        }

        // Edit Profile GET
        [HttpGet]
        public IActionResult Edit()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(user);
        }

        // Edit Profile POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User updatedUser)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid input. Please check the provided information.");
                return View(updatedUser);
            }

            // Update user details
            user.Email = updatedUser.Email;
            user.Address = updatedUser.Address;
            user.PhoneNumber = updatedUser.PhoneNumber;

            _context.SaveChanges();

            TempData["SuccessMessage"] = "Profile updated successfully!";
            return RedirectToAction("Profile");
        }

        // Handle Unauthorized Access
        private IActionResult RedirectToLogin()
        {
            TempData["ErrorMessage"] = "You need to log in to access this page.";
            return RedirectToAction("Login", "Account");
        }
    }
}
