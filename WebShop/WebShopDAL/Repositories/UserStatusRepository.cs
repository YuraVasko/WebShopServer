using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDAL.EntityFramework;
using WebShopDAL.Interfaces;
using WebShopDAL.Models;

namespace WebShopDAL.Repositories
{
    class UserStatusRepository : IRepository<UserStatus>
    {
        private Context _context;

        public UserStatusRepository(Context context)
        {
            _context = context;
        }

        public void Create(UserStatus item)
        {
            _context.UserStatuses.Add(item);
        }

        public void Delete(int id)
        {
            var userStatus = _context.UserStatuses.FirstOrDefault();
            if (userStatus != null)
            {
                _context.UserStatuses.Remove(userStatus);
            }
        }

        public IEnumerable<UserStatus> Get(Func<UserStatus, bool> condition)
        {
            return _context.UserStatuses.Where(condition);
        }

        public UserStatus Get(int id)
        {
            return _context.UserStatuses.Find(id);
        }

        public IEnumerable<UserStatus> GetAll()
        {
            return _context.UserStatuses;
        }

        public void Update(UserStatus item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public IQueryable<UserStatus> GetQuery()
        {
            return _context.UserStatuses;
        }
    }
}
