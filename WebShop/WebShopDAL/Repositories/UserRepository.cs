using System;
using System.Collections.Generic;
using System.Linq;
using WebShopDAL.EntityFramework;
using WebShopDAL.Interfaces;
using WebShopDAL.Models;
using System.Data.Entity;

namespace WebShopDAL.Repositories
{
    class UserRepository : IRepository<User>
    {
        private Context _context;

        public UserRepository(Context context)
        {
            _context = context;
        }

        public void Create(User item)
        {
            _context.Users.Add(item);
        }

        public void Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
        }

        public IEnumerable<User> Get(Func<User, bool> condition)
        {
            return _context.Users
                .Include(u => u.Basket)
                .Include(u => u.UserStatus)
                .Include(u => u.Purchases)
                .Include(u => u.UserRole)
                .Where(condition);
        }

        public User Get(int id)
        {
            return _context.Users
                .Include(u => u.Basket)
                .Include(u => u.UserStatus)
                .Include(u => u.Purchases)
                .Include(u => u.UserRole)
                .FirstOrDefault(u=>u.UserId == id);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users
                .Include(u => u.Basket)
                .Include(u=>u.UserStatus)
                .Include(u=>u.Purchases)
                .Include(u=>u.UserRole);
        }

        public void Update(User item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public IQueryable<User> GetQuery()
        {
            return _context.Users;
        }
    }
}
