using System.Collections.Generic;
using System.Linq;
using WebShopDAL.Models;
using WebShopDto;

namespace WebShopBLL.DtoMappers
{
    class ItemMapper
    {
        private DiscountMapper _discountMapper = new DiscountMapper();
        private ItemCategoryMapper _itemCategoryMapper = new ItemCategoryMapper();

        public ItemDTO GetItemDTOFromItemModel(Item item)
        {
            var categories = new List<ItemCategoryDTO>();
            item.Categories.ToList().ForEach(c =>
            {
                categories.Add(_itemCategoryMapper.GetItemCategoryDTOFromCategoryModel(c));
            });
            return new ItemDTO
            {
                ItemId = item.ItemId,
                Categories = categories,
                AvailableCount = item.AvailableCount,
                Descripton = item.Descripton,
                Price = item.Price,
                Title = item.Title,
                Discount = _discountMapper.GetDiscountDTOFromDiscountModel(item.Discount)
            };
        }    
    }
}
