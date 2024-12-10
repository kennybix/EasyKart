using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.Data;
using System.Linq;

namespace OnlineShopping.Controllers
{
    public class OrderController : BaseController   
    {
        private readonly OnlineShoppingDbContext _context;

        public OrderController(OnlineShoppingDbContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //public IActionResult Index()
        //{
        //    if (HttpContext.Session.GetInt32("UserId") == null)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }

        //    var orders = _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).ToList();
        //    return View(orders);
        //}
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var orders = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == userId)
                .ToList();

            return View(orders);
        }


        public IActionResult Details(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var order = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.Id == id && o.UserId == userId);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}