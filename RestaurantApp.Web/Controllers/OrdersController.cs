using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Entities;
using RestaurantApp.Web.Helpers;
using RestaurantApp.Web.Models;
using RestaurantApp.Web.Models.CartUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApp.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly RestaurantDbContext _context;

        public OrdersController(RestaurantDbContext context)
        {
            _context = context;
        }

        public IActionResult Checkout(decimal value)
        {
            var url = Request.Path.Value.ToString();
            var amount = url.Substring(17);
            ViewBag.Amount = amount;
            return View();
        }

        [HttpPost]
        public IActionResult Checkout([Bind("OrderId,Name,Address,Phone,Mail,RegisterDate,Amount")] Orders order)
        {
            if (ModelState.IsValid)
            {
                _context.Orders.Add(order);
                _context.SaveChanges();

                CreateOrder(order);

                return RedirectToAction("Complete", new { id = order.OrderId });
            }

            return View(order);
        }


        public IActionResult Complete(int id)
        {
            bool isValid = _context.Orders.Any(
                o => o.OrderId == id);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }

        public Orders CreateOrder(Orders order)
        {
            decimal orderTotal = 0;
            order.OrderedMenus = new List<OrderedMenus>();

            var cartItems = CartHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");

            foreach (var item in cartItems)
            {
                var orderedProduct = new OrderedMenus
                {
                    MenuId = item.Menu.Id,
                    OrderId = order.OrderId,
                    Quantity = item.Quantity
                };

                orderTotal += (item.Quantity * item.Menu.Price);
                order.OrderedMenus.Add(orderedProduct);
                _context.OrderedMenus.Add(orderedProduct);
            }

            order.Amount = orderTotal;

            _context.SaveChanges();

            return order;
        }
    }
}
