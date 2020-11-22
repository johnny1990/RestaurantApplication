using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantApp.Entities
{
    public class Chef
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

    }
}
