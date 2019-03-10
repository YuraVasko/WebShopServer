using System;
using System.Collections.Generic;
using System.Linq;
using WebShopBLL.DtoMappers;
using WebShopDAL.Interfaces;
using WebShopDto;

namespace WebShopBLL.Services
{
    class ItemCategoryService
    {
        private IUnitOfWork _webShop;
        private ItemCategoryMapper _itemCategory;

        public ItemCategoryService(IUnitOfWork webShop)
        {
            _webShop = webShop;
            _itemCategory = new ItemCategoryMapper();
        }

        public IEnumerable<ItemCategoryDTO> GetAllCategories()
        {
            var result = new List<ItemCategoryDTO>();
            _webShop.ItemCategories.GetAll().ToList().ForEach(c => 
            {
                result.Add(_itemCategory.GetItemCategoryDTOFromCategoryModel(c));
            });
            return result;
        }

        public void AddCategory(ItemCategoryDTO categoryDTO)
        {
            var category = _itemCategory.GetItemCategoryModelFromCategoryDTO(categoryDTO);
            _webShop.ItemCategories.Create(category);
            _webShop.Save();
        }

        public void UpdateCategory(ItemCategoryDTO categoryDTO)
        {
            var category = _itemCategory.GetItemCategoryModelFromCategoryDTO(categoryDTO);
            _webShop.ItemCategories.Update(category);
            _webShop.Save();
        }

        public void DeleteCategory(int id)
        {
            if (_webShop.ItemCategories.Get(id) != null)
            {
                _webShop.ItemCategories.Delete(id);
                _webShop.Save();
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
