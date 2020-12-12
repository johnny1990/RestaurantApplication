using Microsoft.EntityFrameworkCore;
using RestaurantApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApp.Web.Models
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Ignore<OrderedMenus>();
        //    modelBuilder.Entity<OrderedMenus>().HasNoKey();//.ToView(null);
        //}

        public DbSet<Chef> Chefs { get; set; }

        public DbSet<Meal> Meals { get; set; }

        public DbSet<Feedbacks> Feedbacks { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public DbSet<Orders> Orders { get; set; }

        public DbSet<OrderedMenus> OrderedMenus { get; set; }

    }
}
