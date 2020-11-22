using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Entities
{
    public class Meal
    { 
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Comments { get; set; }

        public List<MenuMeal> MenuMeals { get; set; }
    }
}
