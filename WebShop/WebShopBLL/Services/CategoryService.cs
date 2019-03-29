using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebShopDAL.Models;
using WebShopDAL.UnitOfWork;
using WebShopDto;

namespace WebShopBLL.Services
{
    public class CategoryService
    {
        ShopUnitOfWork _shopUnitOfWork;

        public CategoryService(ShopUnitOfWork unitOfWork)
        {
            _shopUnitOfWork = unitOfWork;
        }

        public void AddNewCategory(ItemCategoryDTO category)
        {
            _shopUnitOfWork.ItemCategoryRepository.Create(new ItemCategory
            {
                ItemCategoryDescription = category.CategoryDescription,
                ItemCategoryName = category.CategoryName
            });
        }

        public void DeleteCategoryByName(string categoryName)
        {
            var category = _shopUnitOfWork.ItemCategoryRepository.GetQuery().Where(c => c.ItemCategoryName == categoryName).FirstOrDefault();
            if (category != null)
            {
                _shopUnitOfWork.ItemCategoryRepository.Delete(category);
            }
        }

        public void AddItemToCategory(int id, string categoryName)
        {
            var item = _shopUnitOfWork.ItemRepository.Get(id);
            var category = _shopUnitOfWork.ItemCategoryRepository.GetQuery().Where(c => c.ItemCategoryName == categoryName).FirstOrDefault();
            if (item != null || category != null)
            {
                item.Categories.Add(category);
                _shopUnitOfWork.ItemRepository.Update(item);
            }
        }

        public List<ItemCategoryDTO> GetAllCategories()
        {
            return _shopUnitOfWork.ItemCategoryRepository.GetQuery().Select(c => new ItemCategoryDTO
            {
                CategoryDescription = c.ItemCategoryDescription,
                CategoryName =c.ItemCategoryName
            }).ToList();
        }
    }
}
