using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantApp.Entities
{
    public class MenuMeal
    {
        [Key]
        public int Id { get; set; }
        public int MenuId { get; set; }
        public Menu Menu { get; set; }

        public int MealId { get; set; }
        public Meal Meal { get; set; }
    }
}
