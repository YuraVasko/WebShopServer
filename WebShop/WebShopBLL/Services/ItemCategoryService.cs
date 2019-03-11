using System;
using System.Collections.Generic;
using System.Linq;
using WebShopDAL.Interfaces;
using WebShopDAL.Models;
using WebShopDto;

namespace WebShopBLL.Services
{
    class ItemCategoryService
    {
        private IUnitOfWork _webShop;

        public ItemCategoryService(IUnitOfWork webShop)
        {
            _webShop = webShop;
        }

        public IEnumerable<ItemCategoryDTO> GetAllCategories()
        {
            return _webShop.ItemCategories.GetQuery().Select(c => new ItemCategoryDTO
            {
                CategoryName = c.ItemCategoryName 
            });
        }

        public void AddCategory(ItemCategoryDTO categoryDTO)
        {
            var category = new ItemCategory
            {
                ItemCategoryName = categoryDTO.CategoryName,
                ItemCategoryDescription = categoryDTO.CategoryDescription
            };
            _webShop.ItemCategories.Create(category);
        }

        public void UpdateCategory(ItemCategoryDTO categoryDTO)
        {
            var category = new ItemCategory
            {
                ItemCategoryId = categoryDTO.CategoryId,
                ItemCategoryName = categoryDTO.CategoryName,
                ItemCategoryDescription = categoryDTO.CategoryDescription
            };
            _webShop.ItemCategories.Update(category);
        }

        public void DeleteCategory(int id)
        {
            if (_webShop.ItemCategories.Get(id) != null)
            {
                _webShop.ItemCategories.Delete(id);
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
