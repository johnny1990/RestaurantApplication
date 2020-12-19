using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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


 
    }
}
