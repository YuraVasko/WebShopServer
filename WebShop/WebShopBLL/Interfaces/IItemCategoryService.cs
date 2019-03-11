﻿using System.Collections.Generic;
using WebShopDto;

namespace WebShopBLL.Interfaces
{
    interface IItemCategoryService
    {
        IEnumerable<ItemCategoryDTO> GetAllCategories();
        void AddCategory(ItemCategoryDTO categoryDTO);
        void UpdateCategory(ItemCategoryDTO categoryDTO);
        void DeleteCategory(int id);
    }
}
