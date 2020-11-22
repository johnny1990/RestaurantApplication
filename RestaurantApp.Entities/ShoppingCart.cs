using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantApp.Entities
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }

        public string CartId { get; set; }

        public int MenuId { get; set; }

        public int Count { get; set; }

        public DateTime RegisterDate { get; set; }

        public virtual Menu Menus { get; set; }
    }
}
