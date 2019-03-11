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
        IRepository<User> UserRepository { get; }
        IRepository<UserRole> UserRoleRepository { get; }
        IRepository<UserStatus> UserStatusRepository { get; }
        IRepository<Purchase> PurchaseRepository { get; }
        IRepository<ItemCategory> ItemCategoryRepository { get; }
        IRepository<Item> ItemRepository { get; }
        IRepository<Discount> DiscountRepository { get; }
        IRepository<Basket> BasketRepository { get; }
        void Save();
    }
}
