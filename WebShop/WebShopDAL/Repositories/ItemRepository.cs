using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebShopDAL.EntityFramework;
using WebShopDAL.Interfaces;
using WebShopDAL.Models;

namespace WebShopDAL.Repositories
{
    class ItemRepository: IRepository<Item>
    {
        private Context _context;

        public ItemRepository(Context context)
        {
            _context = context;
        }

        public void Create(Item item)
        {
            _context.Items.Add(item);
        }

        public void Delete(int id)
        {
            var item = _context.Items.FirstOrDefault();
            if (item != null)
            {
                _context.Items.Remove(item);
            }
        }

        public IEnumerable<Item> Get(Func<Item, bool> condition)
        {
            return _context.Items
                .Include(i => i.Discount)
                .Include(i => i.Categories)
                .Where(condition);
        }

        public Item Get(int id)
        {
            return _context.Items
                .Include(i => i.Discount)
                .Include(i => i.Categories)
                .FirstOrDefault(i => i.ItemId == id);
        }

        public IEnumerable<Item> GetAll()
        {
            return _context.Items
                .Include(i=>i.Discount)
                .Include(i=>i.Categories);
        }

        public void Update(Item item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public IQueryable<Item> GetQuery()
        {
            return _context.Items;
        }
    }
}
