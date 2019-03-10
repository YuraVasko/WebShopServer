using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebShopDAL.EntityFramework;
using WebShopDAL.Interfaces;
using WebShopDAL.Models;

namespace WebShopDAL.Repositories
{
    class ItemCategoryRepository : IRepository<ItemCategory>
    {
        private Context _context;

        public ItemCategoryRepository(Context context)
        {
            _context = context;
        }

        public void Create(ItemCategory item)
        {
            _context.ItemCategories.Add(item);
        }

        public void Delete(int id)
        {
            var itemCategory = _context.ItemCategories.FirstOrDefault();
            if (itemCategory != null)
            {
                _context.ItemCategories.Remove(itemCategory);
            }
        }

        public IEnumerable<ItemCategory> Get(Func<ItemCategory, bool> condition)
        {
            return _context.ItemCategories.Where(condition);
        }

        public ItemCategory Get(int id)
        {
            return _context.ItemCategories.Find(id);
        }

        public IEnumerable<ItemCategory> GetAll()
        {
            return _context.ItemCategories;
        }

        public void Update(ItemCategory item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public IQueryable<ItemCategory> GetQuery()
        {
            return _context.ItemCategories;
        }
    }
}
