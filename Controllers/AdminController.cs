//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using OnlineShopping.Data;
//using OnlineShopping.Models;

//namespace OnlineShopping.Controllers
//{
//    [Route("admin")]
//    public class AdminController : BaseController
//    {
//        private readonly OnlineShoppingDbContext _context;

//        public AdminController(OnlineShoppingDbContext context)
//        {
//            _context = context;
//        }

//        [HttpGet]
//        public IActionResult AdminLogin()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult AdminLogin(string pin)
//        {
//            const string AdminPin = "1234"; // Replace with secure logic
//            if (pin == AdminPin)
//            {
//                HttpContext.Session.SetString("IsAdmin", "true");
//                return RedirectToAction("ManageProducts");
//            }
//            ModelState.AddModelError("", "Invalid PIN");
//            return View();
//        }



//        [HttpGet("products")]
//        public IActionResult ManageProducts()
//        {
//            if (HttpContext.Session.GetString("IsAdmin") != "true")
//            {
//                return RedirectToAction("AdminLogin");
//            }

//            var products = _context.Products.ToList();
//            return View(products);
//        }

//        [HttpGet("products/create")]
//        public IActionResult CreateProduct()
//        {
//            if (HttpContext.Session.GetString("IsAdmin") != "true")
//            {
//                return RedirectToAction("AdminLogin");
//            }

//            return View();
//        }

//        [HttpPost("products/create")]
//        public IActionResult CreateProduct(Product product)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Products.Add(product);
//                _context.SaveChanges();
//                return RedirectToAction("ManageProducts");
//            }
//            return View(product);
//        }

//        [HttpGet("users")]
//        public IActionResult ManageUsers()
//        {
//            var users = _context.Users.ToList();
//            return View(users);
//        }

//        [HttpGet("orders")]
//        public IActionResult ManageOrders()
//        {
//            var orders = _context.Orders.Include(o => o.User).Include(o => o.OrderItems).ThenInclude(oi => oi.Product).ToList();
//            return View(orders);
//        }
//    }
//}

//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using OnlineShopping.Data;
//using OnlineShopping.Models;

//namespace OnlineShopping.Controllers
//{
//    [Route("admin")]
//    public class AdminController : BaseController
//    {
//        private readonly OnlineShoppingDbContext _context;

//        public AdminController(OnlineShoppingDbContext context)
//        {
//            _context = context;
//        }

//        // Admin Login
//        [HttpGet]
//        public IActionResult Login()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Login(string pin)
//        {
//            const string AdminPin = "1234"; // Replace with secure logic
//            if (pin == AdminPin)
//            {
//                HttpContext.Session.SetString("IsAdmin", "true");
//                return RedirectToAction("Dashboard");
//            }
//            ModelState.AddModelError("", "Invalid PIN");
//            return View();
//        }

//        // Dashboard
//        [HttpGet("dashboard")]
//        public IActionResult Dashboard()
//        {
//            if (HttpContext.Session.GetString("IsAdmin") != "true")
//            {
//                return RedirectToAction("AdminLogin");
//            }

//            ViewBag.ProductCount = _context.Products.Count();
//            ViewBag.UserCount = _context.Users.Count();
//            ViewBag.OrderCount = _context.Orders.Count();
//            return View();
//        }

//        // Manage Products
//        [HttpGet("products")]
//        public IActionResult ManageProducts()
//        {
//            if (HttpContext.Session.GetString("IsAdmin") != "true")
//            {
//                return RedirectToAction("AdminLogin");
//            }

//            var products = _context.Products.ToList();
//            return View(products);
//        }

//        [HttpGet("products/create")]
//        public IActionResult CreateProduct()
//        {
//            if (HttpContext.Session.GetString("IsAdmin") != "true")
//            {
//                return RedirectToAction("AdminLogin");
//            }

//            return View();
//        }

//        [HttpPost("products/create")]
//        public IActionResult CreateProduct(Product product)
//        {
//            if (HttpContext.Session.GetString("IsAdmin") != "true")
//            {
//                return RedirectToAction("AdminLogin");
//            }

//            if (ModelState.IsValid)
//            {
//                _context.Products.Add(product);
//                _context.SaveChanges();
//                return RedirectToAction("ManageProducts");
//            }
//            return View(product);
//        }

//        [HttpPost("products/update-stock")]
//        public IActionResult UpdateStock(int productId, int newStock)
//        {
//            if (HttpContext.Session.GetString("IsAdmin") != "true")
//            {
//                return RedirectToAction("AdminLogin");
//            }

//            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
//            if (product != null)
//            {
//                product.Stock = newStock;
//                _context.SaveChanges();
//            }
//            return RedirectToAction("ManageProducts");
//        }

//        [HttpPost("products/delete")]
//        public IActionResult DeleteProduct(int productId)
//        {
//            if (HttpContext.Session.GetString("IsAdmin") != "true")
//            {
//                return RedirectToAction("AdminLogin");
//            }

//            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
//            if (product != null)
//            {
//                _context.Products.Remove(product);
//                _context.SaveChanges();
//            }
//            return RedirectToAction("ManageProducts");
//        }

//        // Manage Users
//        [HttpGet("users")]
//        public IActionResult ManageUsers()
//        {
//            if (HttpContext.Session.GetString("IsAdmin") != "true")
//            {
//                return RedirectToAction("AdminLogin");
//            }

//            var users = _context.Users.ToList();
//            return View(users);
//        }

//        // Manage Orders
//        [HttpGet("orders")]
//        public IActionResult ManageOrders()
//        {
//            if (HttpContext.Session.GetString("IsAdmin") != "true")
//            {
//                return RedirectToAction("AdminLogin");
//            }

//            var orders = _context.Orders.Include(o => o.User).Include(o => o.OrderItems).ThenInclude(oi => oi.Product).ToList();
//            return View(orders);
//        }

//        // Logout
//        [HttpPost("logout")]
//        public IActionResult Logout()
//        {
//            HttpContext.Session.Remove("IsAdmin");
//            return RedirectToAction("AdminLogin");
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.Data;
using OnlineShopping.Models;

namespace OnlineShopping.Controllers
{
    [Route("admin")]
    public class AdminController : BaseController
    {
        private readonly OnlineShoppingDbContext _context;

        public AdminController(OnlineShoppingDbContext context)
        {
            _context = context;
        }

        // Admin Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            const string AdminUsername = "admin01";
            const string AdminPassword = "sky12345";

            if (username == AdminUsername && password == AdminPassword)
            {
                HttpContext.Session.SetString("IsAdmin", "true");
                HttpContext.Session.SetString("AdminName", AdminUsername); // Store admin name in session
                return RedirectToAction("Dashboard");
            }

            ModelState.AddModelError("", "Invalid username or password.");
            return View();
        }

        private bool IsAdminLoggedIn()
        {
            return HttpContext.Session.GetString("IsAdmin") == "true";
        }
        // Dashboard
        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login");
            }

            ViewBag.ProductCount = _context.Products.Count();
            ViewBag.UserCount = _context.Users.Count();
            ViewBag.OrderCount = _context.Orders.Count();
            return View();
        }

        // Manage Products
        [HttpGet("products")]
        public IActionResult ManageProducts()
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Login");
            }

            var products = _context.Products.ToList();
            return View(products);
        }

        [HttpGet("products/create")]
        public IActionResult CreateProduct()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        [HttpPost("products/create")]
        public IActionResult CreateProduct(Product product)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToAction("Login");
            }

            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("ManageProducts");
            }
            return View(product);
        }

        [HttpPost("products/update-stock")]
        public IActionResult UpdateStock(int productId, int newStock)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToAction("Login");
            }

            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                product.Stock = newStock;
                _context.SaveChanges();
            }
            return RedirectToAction("ManageProducts");
        }

        [HttpPost("products/delete")]
        public IActionResult DeleteProduct(int productId)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToAction("Login");
            }

            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return RedirectToAction("ManageProducts");
        }

        // Manage Users
        [HttpGet("users")]
        public IActionResult ManageUsers()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToAction("Login");
            }

            var users = _context.Users.ToList();
            return View(users);
        }

        // Manage Orders
        [HttpGet("orders")]
        public IActionResult ManageOrders()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToAction("Login");
            }

            var orders = _context.Orders.Include(o => o.User).Include(o => o.OrderItems).ThenInclude(oi => oi.Product).ToList();
            return View(orders);
        }

        // Logout
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("IsAdmin");
            HttpContext.Session.Remove("AdminName");
            return RedirectToAction("Login");
        }

    }
}


