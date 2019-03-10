using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebShopDAL.EntityFramework;
using WebShopDAL.Interfaces;
using WebShopDAL.Models;

namespace WebShopDAL.Repositories
{
    class UserRoleRepository: IRepository<UserRole>
    {
        private Context _context;

        public UserRoleRepository(Context context)
        {
            _context = context;
        }

        public void Create(UserRole item)
        {
            _context.UserRoles.Add(item);
        }

        public void Delete(int id)
        {
            var userRole = _context.UserRoles.FirstOrDefault();
            if (userRole != null)
            {
                _context.UserRoles.Remove(userRole);
            }
        }

        public IEnumerable<UserRole> Get(Func<UserRole, bool> condition)
        {
            return _context.UserRoles.Where(condition);
        }

        public UserRole Get(int id)
        {
            return _context.UserRoles.Find(id);
        }

        public IEnumerable<UserRole> GetAll()
        {
            return _context.UserRoles;
        }

        public void Update(UserRole item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public IQueryable<UserRole> GetQuery()
        {
            return _context.UserRoles;
        }
    }
}
