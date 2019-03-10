using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebShopDAL.EntityFramework;
using WebShopDAL.Interfaces;
using WebShopDAL.Models;

namespace WebShopDAL.Repositories
{
    class DiscountRepository : IRepository<Discount>
    {
        private Context _context;

        public DiscountRepository(Context context)
        {
            _context = context;
        }

        public void Create(Discount item)
        {
            _context.Discounts.Add(item);
        }

        public void Delete(int id)
        {
            var discount = _context.Discounts.FirstOrDefault();
            if (discount != null)
            {
                discount.Items.Clear();
                _context.Discounts.Remove(discount);
            }
        }

        public IEnumerable<Discount> Get(Func<Discount, bool> condition)
        {
            return _context.Discounts.Include(d => d.Items).Where(condition);
        }

        public Discount Get(int id)
        {
            return _context.Discounts.Include(d => d.Items).FirstOrDefault(d => d.DiscountId == id);
        }

        public IEnumerable<Discount> GetAll()
        {
            return _context.Discounts.Include(d => d.Items);
        }

        public void Update(Discount item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public IQueryable<Discount> GetQuery()
        {
            return _context.Discounts;
        }
    }
}
