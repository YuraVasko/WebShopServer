using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDAL.Models;

namespace WebShopDAL.EntityFramework
{
    public class Context: DbContext
    {
        public Context() : base("DbConection") { }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserStatus> UserStatuses { get; set; }

        static Context()
        {
            //Database.SetInitializer<Context>(new CustomInitializer());
        }
    }
}
