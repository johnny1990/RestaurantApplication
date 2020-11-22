using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantApp.Entities
{
    public class Feedbacks
    {
        [Key]
        public int Id { get; set; }
        public string Comments { get; set; }

    }
}
