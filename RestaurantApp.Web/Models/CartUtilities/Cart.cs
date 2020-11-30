using RestaurantApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApp.Web.Models.CartUtilities
{
    public class Cart
    {
        public List<ShoppingCart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}
