using System.Collections.Generic;
using System.Data.Entity;
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
                Description = d.DiscountDesription,
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
                DiscountDesription = newDiscount.Description,
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
            var item = _shopUnitOfWork.ItemRepository.GetQuery().Include(i=>i.Discount).FirstOrDefault(i=>i.ItemId == itemId);
            int discountId = item.Discount.DiscountId;
            var discount = _shopUnitOfWork.DiscountRepository.Get(discountId);

            discount.Items.Remove(item);
            item.Discount = null;
            _shopUnitOfWork.Save();
            _shopUnitOfWork.ItemRepository.Update(item);
        }
    }
}
