using RestaurantApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApp.Web.Models.CartUtilities
{
    public class CartItem
    {
        public Menu Menu { get; set; }

        public int Quantity { get; set; }
    }
}
