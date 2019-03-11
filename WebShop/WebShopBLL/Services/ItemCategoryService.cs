using System;
using System.Collections.Generic;
using System.Linq;
using WebShopDAL.Interfaces;
using WebShopDAL.Models;
using WebShopDto;
using WebShopBLL.Interfaces;

namespace WebShopBLL.Services
{
    class ItemCategoryService: IItemCategoryService
    {
        private IUnitOfWork _webShop;

        public ItemCategoryService(IUnitOfWork webShop)
        {
            _webShop = webShop;
        }

        public IEnumerable<ItemCategoryDTO> GetAllCategories()
        {
            return _webShop.ItemCategoryRepository.GetQuery().Select(c => new ItemCategoryDTO
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
            _webShop.ItemCategoryRepository.Create(category);
        }

        public void UpdateCategory(ItemCategoryDTO categoryDTO)
        {
            var category = new ItemCategory
            {
                ItemCategoryId = categoryDTO.CategoryId,
                ItemCategoryName = categoryDTO.CategoryName,
                ItemCategoryDescription = categoryDTO.CategoryDescription
            };
            _webShop.ItemCategoryRepository.Update(category);
        }

        public void DeleteCategory(int id)
        {
            if (_webShop.ItemCategoryRepository.Get(id) != null)
            {
                _webShop.ItemCategoryRepository.Delete(id);
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
