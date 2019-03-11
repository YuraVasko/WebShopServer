using System;
using WebShopDAL.EntityFramework;
using WebShopDAL.Interfaces;
using WebShopDAL.Models;
using WebShopDAL.Repositories;

namespace WebShopDAL.UnitOfWork
{
    public class ShopUnitOfWork
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

        public IRepository<User> UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new GenericRepository<User>(db);
                return _userRepository;
            }
        }

        public IRepository<Basket> BasketRepository
        {
            get
            {
                if (_basketsRepository == null)
                    _basketsRepository = new GenericRepository<Basket>(db);
                return _basketsRepository;
            }
        }

        public IRepository<Discount> DiscountRepository
        {
            get
            {
                if (_discountRepository == null)
                    _discountRepository = new GenericRepository<Discount>(db);
                return _discountRepository;
            }
        }

        public IRepository<ItemCategory> ItemCategoryRepository
        {
            get
            {
                if (_itemCategoryRepository == null)
                    _itemCategoryRepository = new GenericRepository<ItemCategory>(db);
                return _itemCategoryRepository;
            }
        }

        public IRepository<Item> ItemRepository
        {
            get
            {
                if (_itemRepository == null)
                    _itemRepository = new GenericRepository<Item>(db);
                return _itemRepository;
            }
        }

        public IRepository<Purchase> PurchaseRepository
        {
            get
            {
                if (_purchaseRepository == null)
                    _purchaseRepository = new GenericRepository<Purchase>(db);
                return _purchaseRepository;
            }
        }

        public IRepository<UserRole> UserRoleRepository
        {
            get
            {
                if (_userRolesRepository == null)
                    _userRolesRepository = new GenericRepository<UserRole>(db);
                return _userRolesRepository;
            }
        }

        public IRepository<UserStatus> UserStatusRepository
        {
            get
            {
                if (_userStatusesRepository == null)
                    _userStatusesRepository = new GenericRepository<UserStatus>(db);
                return _userStatusesRepository;
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}

