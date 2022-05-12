using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantApp.Entities;
using RestaurantApp.Web.Models;
using RestaurantApp.Web.Models.IP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly RestaurantDbContext _context;

        public HomeController( RestaurantDbContext context)
        {
            _context = context;
        }

        //restaurant dashboard
        [ServiceFilter(typeof(TraceIP))]
        public IActionResult Index()
        {
            try
            {
                var restaurantDbContext = _context.Menus.Include(m => m.Chef);
                return View(restaurantDbContext.ToList());
            }
            catch (Exception ex)
            {
                Logger.LogWriter.LogException(ex);
                return NotFound();
            }
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SocialMedia()
        {
            return View();
        }

        public  IActionResult Feedbacks()
        {
            return View(_context.Feedbacks.ToList());
        }


        public IActionResult NewFeedback()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewFeedback([Bind("Id,Comments")] Feedbacks feedbacks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feedbacks);
                _context.SaveChanges();
                return RedirectToAction(nameof(Feedbacks));
            }
            return View(feedbacks);
        }

        [HttpGet]
        public IActionResult Statistics()
        {

            var nrOrders = _context.Orders.Select(p => p.OrderId);
            ViewBag.NrOrders = nrOrders.Count();

            var orderedmenus = _context.OrderedMenus.Select(p => p.OrderId);
            ViewBag.NrMenus = orderedmenus.Count();

            var vouchers = _context.Vouchers.Select(p => p.Id);
            ViewBag.Vouchers = vouchers.Count();

            return View();          
        }

        [HttpGet]
        [Authorize]
        public IActionResult MyOrders()
        {
            try
            {
                var UserName = User.Identity.Name.ToString();

                IQueryable<Orders> order = from p in _context.Orders
                                          where p.Mail == UserName
                                          select p;
                ViewBag.Orders = order.ToList();
                return View();
            }
            catch (Exception ex)
            {
                Logger.LogWriter.LogException(ex);
                return NotFound();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
