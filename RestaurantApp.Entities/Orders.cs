using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantApp.Entities
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Mail { get; set; }

        public DateTime RegisterDate { get; set; }

        public decimal Amount { get; set; }

        public virtual ICollection<OrderedMenus> OrderedMenus { get; set; }
    }
}
