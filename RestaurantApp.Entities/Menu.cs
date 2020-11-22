using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantApp.Entities
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ChefId { get; set; }
        public Chef Chef { get; set; }
        public decimal Price { get; set; }
        public List<MenuMeal> MenuMeals { get; set; }

        public int MealsCount => MenuMeals.Count;
    }
}
