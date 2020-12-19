using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly RestaurantDbContext _context;
        public string ShoppingCartId { get; set; }

        public ShoppingCartController(RestaurantDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            try
            {
                var cart = CartHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                ViewBag.Cart = cart;
                if (ViewBag.Cart == null)
                {
                    return View();
                }

                ViewBag.total = cart.Sum(item => item.Menu.Price * item.Quantity);
                return View();
            }
            catch (Exception ex)
            {
                Logger.LogWriter.LogException(ex);
                return NotFound();
            }          
        }

        public IViewComponentResult Summary()
        {
            var cart = CartHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");

            ViewData["CartCount"] = cart.Count();
            return (IViewComponentResult)View("Summary");
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            Menu menu = new Menu();
            if (CartHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                List<CartItem> cart = new List<CartItem>();
                cart.Add(new CartItem { Menu = await _context.Menus.FindAsync(id), Quantity = 1 });
                CartHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
               

                //var cartItem = _context.ShoppingCarts.SingleOrDefault(c => c.CartId == ShoppingCartId && c.MenuId == menu.Id);

                // cartItem = new ShoppingCart
                //{
                //    MenuId = id,
                //    CartId = ShoppingCartId,
                //    Count = 1,
                //    RegisterDate = DateTime.Now
                //};
                //_context.ShoppingCarts.Add(cartItem);
                //_context.SaveChanges();
                
            }
            else
            {
                List<CartItem> cart = CartHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                int index = isExistingItem(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                    //
                    //var cartItem = _context.ShoppingCarts.SingleOrDefault(c => c.CartId == ShoppingCartId && c.MenuId == menu.Id);

                    //cartItem = new ShoppingCart
                    //{
                    //    MenuId = id,
                    //    CartId = ShoppingCartId,
                    //    Count = 1,
                    //    RegisterDate = DateTime.Now
                    //};
                    //_context.ShoppingCarts.Add(cartItem);
                    //_context.SaveChanges();
                    //
                }
                else
                {
                    cart.Add(new CartItem { Menu = await _context.Menus.FindAsync(id), Quantity = 1 });
                }
                CartHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult DeleteFromCart(int id)
        {
            List<CartItem> cart = CartHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            int index = isExistingItem(id);
            cart.RemoveAt(index);

      
            //var cartItem = _context.ShoppingCarts.SingleOrDefault(cart => cart.CartId == ShoppingCartId && cart.MenuId == id);
            //_context.ShoppingCarts.Remove(cartItem);
            //_context.SaveChanges();

            CartHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }


        private int isExistingItem(int id)
        {
            List<CartItem> cart = CartHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Menu.Id.Equals(id))
                {
                    return i;
                }

            }

            return -1;
        }
    }
}
