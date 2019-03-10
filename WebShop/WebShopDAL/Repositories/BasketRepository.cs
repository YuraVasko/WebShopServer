using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebShopDAL.EntityFramework;
using WebShopDAL.Interfaces;
using WebShopDAL.Models;

namespace WebShopDAL.Repositories
{
    class BasketRepository : IRepository<Basket>
    {
        private Context _context;

        public BasketRepository(Context context)
        {
            _context = context;
        }

        public void Create(Basket item)
        {
            _context.Baskets.Add(item);
        }

        public void Delete(int id)
        {
            var basket = _context.Baskets.FirstOrDefault();
            if (basket != null)
            {
                _context.Baskets.Remove(basket);
            }
        }

        public IEnumerable<Basket> Get(Func<Basket, bool> condition)
        {
            return _context.Baskets.Include(b => b.Items).Where(condition);
        }

        public Basket Get(int id)
        {
            return _context.Baskets.Include(b => b.Items).FirstOrDefault(b => b.BasketId == id);
        }

        public IEnumerable<Basket> GetAll()
        {
            return _context.Baskets.Include(b => b.Items);
        }

        public void Update(Basket item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public IQueryable<Basket> GetQuery()
        {
            return _context.Baskets;
        }
    }
}
