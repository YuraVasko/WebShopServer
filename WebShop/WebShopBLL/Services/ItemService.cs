using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebShopDAL.Models;
using WebShopDAL.UnitOfWork;
using WebShopDto;

namespace WebShopBLL.Services
{
    public class ItemService
    {
        ShopUnitOfWork _shopUnitOfWork;

        public ItemService(ShopUnitOfWork shopUnitOfWork)
        {
            _shopUnitOfWork = shopUnitOfWork;
        }

        public void AddNewItem(ItemDTO newItem)
        {
            List<ItemCategory> itemCategories = _shopUnitOfWork.ItemCategoryRepository.Get(c => newItem.Categories.Contains(c.ItemCategoryName)).ToList();

            var item = new Item
            {
                Title = newItem.Title,
                Price = newItem.Price,
                AvailableCount = newItem.AvailableCount,
                Descripton = newItem.Description,
                Categories = itemCategories
            };

            _shopUnitOfWork.ItemRepository.Create(item);
        }

        public void DeleteItemById(int itemId)
        {
            var item = _shopUnitOfWork.ItemRepository.Get(itemId);
            if (item != null)
            {
                _shopUnitOfWork.ItemRepository.Delete(item);
            }
        }

        public List<ItemDTO> GetItemsFromPage(int pageNumber)
        {
            int n = pageNumber - 1;
            return _shopUnitOfWork.ItemRepository.GetQuery().OrderBy(i=>i.ItemId).Skip(n*9).Take(9).Select(i =>
                new ItemDTO
                {
                    Title = i.Title,
                    ItemId = i.ItemId,
                    Price = i.Price,
                    Description = i.Descripton,
                    Discount = new DiscountDTO
                    {
                        Id = i.Discount != null ? i.Discount.DiscountId : 0,
                        Percentage = i.Discount != null ? i.Discount.DiscountPercentage : 0
                    }
                }
            ).ToList();
        }

        public List<ItemDTO> GetAllItemsByCategory(string categoryName)
        {
            var category = _shopUnitOfWork.ItemCategoryRepository.GetQuery().Where(c => c.ItemCategoryName == categoryName).FirstOrDefault();
            return _shopUnitOfWork.ItemRepository.GetQuery().Where(c => c.Categories.Contains(category)).Select(i =>
                   new ItemDTO
                   {
                       Title = i.Title,
                       ItemId = i.ItemId,
                       Price = i.Price,
                       Discount = new DiscountDTO
                       {
                           Id = i.Discount.DiscountId,
                           Percentage = i.Discount.DiscountId
                       }
                   }
            ).ToList();
        }

        public ItemDTO GetItemDetails(int id)
        {
            var item = _shopUnitOfWork.ItemRepository.Get(id);
            return new ItemDTO
            {
                AvailableCount = item.AvailableCount,
                Description = item.Descripton,
                ItemId = item.ItemId,
                Price = item.Price,
                Title = item.Title,
                Categories = item.Categories != null ? item.Categories.Select(i => i.ItemCategoryName).ToList() : null,
                Discount = new DiscountDTO
                {
                    Description = item.Discount != null ? item.Discount.DiscountDesription : string.Empty,
                    Id = item.Discount != null ? item.Discount.DiscountId : 0,
                    Percentage = item.Discount != null ? item.Discount.DiscountId : 0
                }
            };
        }
    }
}
