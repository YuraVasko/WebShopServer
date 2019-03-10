using System;
using System.Collections.Generic;
using System.Linq;
using WebShopBLL.DtoMappers;
using WebShopDAL.Interfaces;
using WebShopDto;

namespace WebShopBLL.Services
{
    public class BasketService
    {
        private IUnitOfWork _webShop;
        private ItemMapper _itemMapper;

        public BasketService(IUnitOfWork webShop)
        {
            _webShop = webShop;
            _itemMapper = new ItemMapper();
        }

        public void AddNewItemInUserBasket(int basketId, int itemId)
        {
            var item = _webShop.Items.Get(itemId);
            var basket = _webShop.Baskets.Get(basketId);
            if (item != null && basket != null)
            {
                basket.Items.Add(item);
                _webShop.Baskets.Update(basket);
                _webShop.Save();
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void RemoveItemFromUserBasket(int basketId, int itemId)
        {
            var item = _webShop.Items.Get(itemId);
            var basket = _webShop.Baskets.Get(basketId);
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
            var basket = _webShop.Baskets.Get(basketId);
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
            var basket = _webShop.Baskets.Get(basketId);
            if (basket != null)
            {
                var result = new List<ItemDTO>();
                var items = basket.Items.ToList();
                items.ForEach(i => result.Add(_itemMapper.GetItemDTOFromItemModel(i)));
                return result;
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
