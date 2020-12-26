using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApp.Entities;
using RestaurantApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace RestaurantApp.Web.Controllers.API
{
    [Route("api/Menus")]
    [ApiController]
    public class MenusApiController : ControllerBase
    {
        private readonly RestaurantDbContext _context;

        public MenusApiController(RestaurantDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetMenus")]
        public IActionResult GetMenus()
        {
            var meals = _context.Menus.Include(m => m.Chef);
            return new OkObjectResult(meals);
        }

        [HttpGet]
        [Route("GetMenuById")]
        public IActionResult GetMenuById(int id)
        {
           
            var meal = from a in _context.Menus.Where(p => p.Id == id).ToList()
                         select new
                         {
                             a.Id,
                             a.Name,
                             a.ChefId,
                             a.Price,
                             a.Meals

                         };
            return new OkObjectResult(meal);
        }

        [HttpPost]
        [Route("CreateMenu")]
        public IActionResult CreateMenu([FromBody] Menu menu)
        {
                _context.Add(menu);
                _context.SaveChanges();
                return CreatedAtAction(nameof(Menu), new { id = menu.Id }, menu);
        }

        [HttpPut]
        [Route("UpdateMenu")]
        public IActionResult UpdateMenu([FromBody] Menu menu)
        {
              _context.Update(menu);
              _context.SaveChanges();
                return new OkResult();               
        }

        [HttpDelete]
        [Route("DeleteMenu")]
        public IActionResult DeleteMenu(int id)
        {
            var menu = _context.Menus.Find(id);
            _context.Menus.Remove(menu);
             _context.SaveChanges();
            return new OkResult();
        }
    }
}
