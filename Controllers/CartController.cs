using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using OnlineShopping.Data;
using OnlineShopping.Models;
using OnlineShopping.Helpers;

namespace OnlineShopping.Controllers
{
    public class CartController : BaseController
    {
        private readonly OnlineShoppingDbContext _context;

        public CartController(OnlineShoppingDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var cart = GetCartItems();
            ViewBag.SubTotal = cart.Sum(ci => ci.Quantity * ci.Price);
            ViewBag.Total = ViewBag.SubTotal + (ViewBag.SubTotal * 0.135m); // 13.5% GST
            return View(cart);
        }

        private List<OrderItem> GetCartItems()
        {
            var cart = HttpContext.Session.Get<List<OrderItem>>("Cart") ?? new List<OrderItem>();
            return cart;
        }

        private void SaveCartItems(List<OrderItem> cart)
        {
            HttpContext.Session.Set("Cart", cart);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var cart = GetCartItems();
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);

            if (product != null)
            {
                var cartItem = cart.FirstOrDefault(ci => ci.ProductId == productId);

                if (cartItem == null)
                {
                    cart.Add(new OrderItem
                    {
                        ProductId = product.Id,
                        Product = product,
                        Quantity = quantity,
                        Price = product.Price
                    });
                }
                else
                {
                    cartItem.Quantity += quantity;
                }

                SaveCartItems(cart);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var cart = GetCartItems();
            var cartItem = cart.FirstOrDefault(ci => ci.ProductId == productId);

            if (cartItem != null)
            {
                cart.Remove(cartItem);
                SaveCartItems(cart);
            }

            return RedirectToAction("Index");
        }

        //public IActionResult Checkout()
        //{
        //    var userId = HttpContext.Session.GetInt32("UserId");
        //    if (userId == null)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }

        //    var cart = GetCartItems();
        //    if (!cart.Any())
        //    {
        //        ViewBag.Message = "Your cart is empty.";
        //        return View();
        //    }

        //    // Create the order and map only the necessary fields
        //    var order = new Order
        //    {
        //        UserId = userId.Value,
        //        OrderDate = DateTime.Now,
        //        TotalAmount = cart.Sum(ci => ci.Quantity * ci.Price),
        //        OrderItems = cart.Select(ci => new OrderItem
        //        {
        //            ProductId = ci.ProductId, // Use ProductId instead of Product
        //            Quantity = ci.Quantity,
        //            Price = ci.Price
        //        }).ToList()
        //    };

        //    _context.Orders.Add(order);
        //    _context.SaveChanges();

        //    SaveCartItems(new List<OrderItem>()); // Clear the cart

        //    return RedirectToAction("Index", "Order");
        //}
        public IActionResult Checkout()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var cart = GetCartItems();
            if (!cart.Any())
            {
                ViewBag.Message = "Your cart is empty.";
                return View("Index");
            }

            // Pass cart details to the payment page
            ViewBag.TotalAmount = cart.Sum(ci => ci.Quantity * ci.Price) + cart.Sum(ci => ci.Quantity * ci.Price) * 0.135m; // Including GST
            return View("Payment");
        }


        [HttpPost]
        public IActionResult ProcessPayment(string NameOnCard, string CardNumber, string ExpiryDate, string CVV)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var cart = GetCartItems();
            if (!cart.Any())
            {
                ViewBag.Message = "Your cart is empty.";
                return RedirectToAction("Index");
            }

            // Validate card details (mock validation for simplicity)
            if (string.IsNullOrEmpty(NameOnCard) || string.IsNullOrEmpty(CardNumber) || string.IsNullOrEmpty(ExpiryDate) || string.IsNullOrEmpty(CVV))
            {
                ViewBag.ErrorMessage = "Invalid payment details. Please try again.";
                return View("Payment");
            }

            // Save order to the database
            var order = new Order
            {
                UserId = userId.Value,
                OrderDate = DateTime.Now,
                TotalAmount = cart.Sum(ci => ci.Quantity * ci.Price) + cart.Sum(ci => ci.Quantity * ci.Price) * 0.135m, // Including GST
                OrderItems = cart.Select(ci => new OrderItem
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    Price = ci.Price
                }).ToList()
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            // Clear the cart
            SaveCartItems(new List<OrderItem>());

            // Show success page or redirect to orders
            ViewBag.Message = "Payment successful! Your order has been placed.";
            return View("PaymentSuccess");
        }

    }
}
