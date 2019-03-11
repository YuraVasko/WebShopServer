using System;
using System.Collections.Generic;
using System.Linq;
using WebShopDAL.Interfaces;
using WebShopDto;

namespace WebShopBLL.Services
{
    public class BasketService
    {
        private IUnitOfWork _webShop;

        public BasketService(IUnitOfWork webShop)
        {
            _webShop = webShop;
        }

        public void AddNewItemInUserBasket(int basketId, int itemId)
        {
            var item = _webShop.ItemRepository.Get(itemId);
            var basket = _webShop.BasketRepository.Get(basketId);
            if (item != null && basket != null)
            {
                basket.Items.Add(item);
                _webShop.BasketRepository.Update(basket);
                _webShop.Save();
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void RemoveItemFromUserBasket(int basketId, int itemId)
        {
            var item = _webShop.ItemRepository.Get(itemId);
            var basket = _webShop.BasketRepository.Get(basketId);
            if (item != null && basket != null)
            {
                bool isItemInBasket = basket.Items.Contains(item);
                if (isItemInBasket)
                {
                    basket.Items.Remove(item);
                    _webShop.Save();
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void ClearBasket(int basketId)
        {
            var basket = _webShop.BasketRepository.Get(basketId);
            if (basket != null)
            {
                basket.Items.Clear();
                _webShop.Save();
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public IEnumerable<ItemDTO> GetItemsFromBasket(int basketId)
        {
            var basket = _webShop.BasketRepository.Get(basketId);
            if (basket != null)
            {
                return basket.Items.Select(i => new ItemDTO
                {
                    ItemId = i.ItemId,
                    AvailableCount = i.AvailableCount,
                    Descripton = i.Descripton,
                    Price = i.Price,
                    Title = i.Title,
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
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
