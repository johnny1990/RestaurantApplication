using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Entities
{
    public class Vouchers
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string VoucherType { get; set; }

        public enum EnumVoucherType { Cent = 0, Dollar = 1 }

        [Required]
        public double Discount { get; set; }

        [Required]
        public double MinimumAmount { get; set; }

        public bool IsActive { get; set; }
    }
}
