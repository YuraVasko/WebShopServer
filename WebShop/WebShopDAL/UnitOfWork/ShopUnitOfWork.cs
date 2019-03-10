using System;
using WebShopDAL.EntityFramework;
using WebShopDAL.Interfaces;
using WebShopDAL.Models;
using WebShopDAL.Repositories;

namespace WebShopDAL.UnitOfWork
{
    public class ShopUnitOfWork : IUnitOfWork
    {
        private Context db = new Context();
        private IRepository<User> _userRepository;
        private IRepository<Basket> _basketsRepository;
        private IRepository<Discount> _discountRepository;
        private IRepository<ItemCategory> _itemCategoryRepository;
        private IRepository<Item> _itemRepository;
        private IRepository<Purchase> _purchaseRepository;
        private IRepository<UserStatus> _userStatusesRepository;
        private IRepository<UserRole> _userRolesRepository;

        public IRepository<User> Users
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(db);
                return _userRepository;
            }
        }

        public IRepository<Basket> Baskets
        {
            get
            {
                if (_basketsRepository == null)
                    _basketsRepository = new BasketRepository(db);
                return _basketsRepository;
            }
        }

        public IRepository<Discount> Discounts
        {
            get
            {
                if (_discountRepository == null)
                    _discountRepository = new DiscountRepository(db);
                return _discountRepository;
            }
        }

        public IRepository<ItemCategory> ItemCategories
        {
            get
            {
                if (_itemCategoryRepository == null)
                    _itemCategoryRepository = new ItemCategoryRepository(db);
                return _itemCategoryRepository;
            }
        }

        public IRepository<Item> Items
        {
            get
            {
                if (_itemRepository == null)
                    _itemRepository = new ItemRepository(db);
                return _itemRepository;
            }
        }

        public IRepository<Purchase> Purchases
        {
            get
            {
                if (_purchaseRepository == null)
                    _purchaseRepository = new PurchaseRepository(db);
                return _purchaseRepository;
            }
        }

        public IRepository<UserRole> UserRoles
        {
            get
            {
                if (_userRolesRepository == null)
                    _userRolesRepository = new UserRoleRepository(db);
                return _userRolesRepository;
            }
        }

        public IRepository<UserStatus> UserStatuses
        {
            get
            {
                if (_userStatusesRepository == null)
                    _userStatusesRepository = new UserStatusRepository(db);
                return _userStatusesRepository;
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

