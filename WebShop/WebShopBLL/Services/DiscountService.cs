using System.Collections.Generic;
using System.Linq;
using WebShopDAL.Models;
using WebShopDAL.UnitOfWork;
using WebShopDto;

namespace WebShopBLL.Services
{
    public class DiscountService
    {
        ShopUnitOfWork _shopUnitOfWork;

        public DiscountService(ShopUnitOfWork shopUnitOfWork)
        {
            _shopUnitOfWork = shopUnitOfWork;
        }

        public List<DiscountDTO> GetAllDiscounts()
        {
            return _shopUnitOfWork.DiscountRepository.GetQuery().Select(d => new DiscountDTO
            {
                Desription = d.DiscountDesription,
                Percentage = d.DiscountPercentage,
                Id = d.DiscountId
            
            }).ToList();
        }

        public void AddDiscountToItem(int itemId, int discountId)
        {
            var discount = _shopUnitOfWork.DiscountRepository.Get(discountId);
            var item = _shopUnitOfWork.ItemRepository.Get(itemId);
            item.Discount = discount;
            _shopUnitOfWork.ItemRepository.Update(item);
        }

        public void AddNewDiscount(DiscountDTO newDiscount)
        {
            _shopUnitOfWork.DiscountRepository.Create(new Discount
            {
                DiscountDesription = newDiscount.Desription,
                DiscountPercentage = newDiscount.Percentage
            });
        }

        public void DeleteDiscount(int discountId)
        {
            var discount =_shopUnitOfWork.DiscountRepository.Get(discountId);
            if(discount != null)
            {
                _shopUnitOfWork.DiscountRepository.Delete(discount);
            }
        }

        public void DeleteItemDiscount(int itemId)
        {
            var item = _shopUnitOfWork.ItemRepository.Get(itemId);
            item.Discount = null;
            _shopUnitOfWork.ItemRepository.Update(item);
        }
    }
}
