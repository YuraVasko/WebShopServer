using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using WebShopDAL.Models;
using WebShopDAL.UnitOfWork;
using WebShopDto;

namespace WebShopBLL.Services
{
    public class CartService
    {
        ShopUnitOfWork _shopUnitOfWork;

        public CartService(ShopUnitOfWork unitOfWork)
        {
            _shopUnitOfWork = unitOfWork;
        }

        public List<ItemDTO> GetUserCart(string userName, int pageNumber)
        {
            var user = _shopUnitOfWork.UserRepository.GetQuery().FirstOrDefault(u => u.Email == userName);
            int n = pageNumber - 1;
            if (!user.BasketId.HasValue)
            {
                Basket newBasket = new Basket();
                newBasket.Items = new List<Item>();
                _shopUnitOfWork.BasketRepository.Create(newBasket);
                user.Basket = newBasket;
            }
            var basket = _shopUnitOfWork.BasketRepository.GetQuery().Include(b => b.Items).FirstOrDefault(b => b.BasketId == user.BasketId);

            var itemIds = basket.Items.Select(i => i.ItemId).ToList();

            var items = _shopUnitOfWork.ItemRepository.GetQuery().OrderBy(i => i.ItemId).Skip(n*9).Take(9).Include(i => i.Discount).Where(i => itemIds.Contains(i.ItemId)).ToList();

            return items.Select(i => new ItemDTO
            {
                Title = i.Title,
                Description = i.Descripton,
                Price = i.Price,
                ItemId = i.ItemId,
                Discount = new DiscountDTO
                {
                    Description = i.Discount!= null ? i.Discount.DiscountDesription : string.Empty,
                    Percentage = i.Discount != null ? i.Discount.DiscountPercentage : 0
                }
            }).ToList();
        }

        public List<ItemDTO> GetUserPurchase(string userName, int pageNumber)
        {
            var user = _shopUnitOfWork.UserRepository.GetQuery().FirstOrDefault(u => u.Email == userName);
            int n = pageNumber - 1;
            if (!user.BasketId.HasValue)
            {
                Basket newBasket = new Basket();
                newBasket.Items = new List<Item>();
                _shopUnitOfWork.BasketRepository.Create(newBasket);
                user.Basket = newBasket;
            }

            var purchases = _shopUnitOfWork.PurchaseRepository.GetQuery().Include(p => p.User).Include(p=>p.Items).Select(p => new {p.User, p.PurchaseId, p.Items}).ToList();

            var itemsCollection = purchases.Where(p => p.User.Id == user.Id).Select(p => p.Items).ToList();

            var items = new List<Item>();
            itemsCollection.ForEach(c => c.ToList().ForEach(i => items.Add(i)));

            return items.Select(i => new ItemDTO
            {
                Title = i.Title,
                Description = i.Descripton,
                Price = i.Price,
                ItemId = i.ItemId,
                Discount = new DiscountDTO
                {
                    Description = i.Discount != null ? i.Discount.DiscountDesription : string.Empty,
                    Percentage = i.Discount != null ? i.Discount.DiscountPercentage : 0
                }
            }).ToList();
        }

        public void SubmitUserPurchase(string userName)
        {
            var user = _shopUnitOfWork.UserRepository.GetQuery().FirstOrDefault(u => u.Email == userName);

            if (!user.BasketId.HasValue)
            {
                Basket newBasket = new Basket();
                newBasket.Items = new List<Item>();
                _shopUnitOfWork.BasketRepository.Create(newBasket);
                user.Basket = newBasket;
            }
            var basket = _shopUnitOfWork.BasketRepository.GetQuery().Include(b => b.Items).FirstOrDefault(b => b.BasketId == user.BasketId);

            var itemIds = basket.Items.Select(i => i.ItemId).ToList();

            var items = _shopUnitOfWork.ItemRepository.GetQuery().Include(i => i.Discount).Where(i => itemIds.Contains(i.ItemId)).ToList();

            Purchase newPurchase = new Purchase();

            newPurchase.Items = items;
            newPurchase.User = user;

            _shopUnitOfWork.PurchaseRepository.Create(newPurchase);

            ClearCart(userName);
        }

        public void ClearCart(string userName)
        {
            var user = _shopUnitOfWork.UserRepository.GetQuery().FirstOrDefault(u => u.Email == userName);

            if(user.BasketId.HasValue)
            {
                var basket = _shopUnitOfWork.BasketRepository.GetQuery().Include(b=>b.Items).FirstOrDefault(b => b.BasketId == user.BasketId);
                basket.Items.Clear();
                _shopUnitOfWork.Save();
            }
        }

        public void DeleteItemFromCart(string userName, int itemId)
        {
            var user = _shopUnitOfWork.UserRepository.GetQuery().FirstOrDefault(u => u.Email == userName);
            if(user.BasketId.HasValue)
            {
                var basket = _shopUnitOfWork.BasketRepository.GetQuery().Include(b=>b.Items).FirstOrDefault(b => b.BasketId == user.BasketId);
                var item = _shopUnitOfWork.ItemRepository.Get(itemId);
                basket.Items.Remove(item);
                _shopUnitOfWork.Save();
            }           
        }

        public void AddItemToCart(string userName, int itemId)
        {
            var user = _shopUnitOfWork.UserRepository.GetQuery().FirstOrDefault(u => u.Email == userName);
            Basket basket;
            if (user.BasketId.HasValue)
            {
                basket = _shopUnitOfWork.BasketRepository.GetQuery().Include(b => b.Items)
                    .FirstOrDefault(b => b.BasketId == user.BasketId.Value);
            }
            else
            {
                basket = new Basket();
                basket.Items = new List<Item>();
                _shopUnitOfWork.BasketRepository.Create(basket);
                user.Basket = basket;
            }

            var item = _shopUnitOfWork.ItemRepository.Get(itemId);
            user.Basket.Items.Add(item);
            _shopUnitOfWork.Save();
        }
    }
}
