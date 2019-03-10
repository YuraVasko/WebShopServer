using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebShopDAL.EntityFramework;
using WebShopDAL.Interfaces;
using WebShopDAL.Models;

namespace WebShopDAL.Repositories
{
    class PurchaseRepository : IRepository<Purchase>
    {
        private Context _context;

        public PurchaseRepository(Context context)
        {
            _context = context;
        }

        public void Create(Purchase item)
        {
            _context.Purchases.Add(item);
        }

        public void Delete(int id)
        {
            var purchase = _context.Purchases.FirstOrDefault();
            if (purchase != null)
            {
                _context.Purchases.Remove(purchase);
            }
        }

        public IEnumerable<Purchase> Get(Func<Purchase, bool> condition)
        {
            return _context.Purchases
                .Include(p => p.Items)
                .Include(p => p.User)
                .Where(condition);
        }

        public Purchase Get(int id)
        {
            return _context.Purchases
                .Include(p => p.Items)
                .Include(p => p.User)
                .FirstOrDefault(p=>p.PurchaseId == id);
        }

        public IEnumerable<Purchase> GetAll()
        {
            return _context.Purchases
                .Include(p => p.Items)
                .Include(p => p.User);
        }

        public void Update(Purchase item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public IQueryable<Purchase> GetQuery()
        {
            return _context.Purchases;
        }
    }
}
