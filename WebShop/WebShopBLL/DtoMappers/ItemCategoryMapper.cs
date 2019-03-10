using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDAL.Models;
using WebShopDto;

namespace WebShopBLL.DtoMappers
{
    class ItemCategoryMapper
    {
        public ItemCategory GetItemCategoryModelFromCategoryDTO(ItemCategoryDTO itemCategory)
        {
            return new ItemCategory
            {
                ItemCategoryId = itemCategory.CategoryId,
                ItemCategoryName = itemCategory.CategoryName,
                ItemCategoryDescription = itemCategory.CategoryDescription
            };
        }

        public ItemCategoryDTO GetItemCategoryDTOFromCategoryModel(ItemCategory itemCategory)
        {
            return new ItemCategoryDTO
            {
                CategoryId = itemCategory.ItemCategoryId,
                CategoryName = itemCategory.ItemCategoryName,
                CategoryDescription = itemCategory.ItemCategoryDescription
            };
        }
    }
}
