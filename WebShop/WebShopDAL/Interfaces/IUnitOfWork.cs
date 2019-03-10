using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDAL.Models;

namespace WebShopDAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<UserRole> UserRoles { get; }
        IRepository<UserStatus> UserStatuses { get; }
        IRepository<Purchase> Purchases { get; }
        IRepository<ItemCategory> ItemCategories { get; }
        IRepository<Item> Items { get; }
        IRepository<Discount> Discounts { get; }
        IRepository<Basket> Baskets { get; }
        void Save();
    }
}
