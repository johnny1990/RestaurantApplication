using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RestaurantApp.Entities
{
    public class OrderedMenus
    {
        [Key]
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }

        public virtual Orders Orders { get; set; }
        public virtual Menu Menus { get; set; }
    }
}
