using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopBLL.Interfaces;
using WebShopDAL.Interfaces;
using WebShopDAL.Models;
using WebShopDto;

namespace WebShopBLL.Services
{
    class ItemService : IItemService
    {
        private IUnitOfWork _webShop;

        public ItemService(IUnitOfWork webShop)
        {
            _webShop = webShop;
        }

        public IEnumerable<ItemDTO> GetAllItems()
        {
            return _webShop.ItemRepository.GetQuery().Select(i => new ItemDTO
            {
                AvailableCount = i.AvailableCount,
                Descripton = i.Descripton,
                Price = i.Price,
                Title = i.Title,
                ItemId = i.ItemId,
                Discount = new DiscountDTO
                {
                    Id = i.Discount.DiscountId,
                    Desription = i.Discount.DiscountDesription,
                    Percentage = i.Discount.DiscountPercentage
                },
                Categories = i.Categories.Select(c => new ItemCategoryDTO
                {
                    CategoryDescription = c.ItemCategoryDescription,
                    CategoryId = c.ItemCategoryId,
                    CategoryName = c.ItemCategoryName
                }).ToArray(),
            });
        }

        public void AddNewItem(ItemDTO newItem)
        {
            _webShop.ItemRepository.Create(new Item
            {
                AvailableCount = newItem.AvailableCount,
                Descripton = newItem.Descripton,
                Price = newItem.Price,
                Title = newItem.Title,
                Baskets = new List<Basket>(),
                Purchases = new List<Purchase>(),
                Categories = newItem.Categories.Select(c => new ItemCategory
                {
                    ItemCategoryDescription = c.CategoryDescription,
                    ItemCategoryId = c.CategoryId,
                    ItemCategoryName = c.CategoryName
                }).ToArray(),
            });
        }

        public void DeleteItem(int id)
        {
            _webShop.ItemRepository.Delete(id);
        }
    }
}
