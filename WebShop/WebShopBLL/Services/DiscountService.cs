using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDAL.Interfaces;
using WebShopDAL.Models;
using WebShopDto;

namespace WebShopBLL.Services
{
    class DiscountService
    {
        private IUnitOfWork _webShop;

        public DiscountService(IUnitOfWork webShop)
        {
            _webShop = webShop;
        }

        public IEnumerable<DiscountDTO> GetAllDiscounts()
        {
            return _webShop.Discounts.GetQuery().Select(d => new DiscountDTO
            {
                Desription= d.DiscountDesription,
                Id= d.DiscountId,
                Percentage = d.DiscountPercentage
            });
        }

        public void AddDiscount(DiscountDTO discountDTO)
        {
            var discount = new Discount
            {
                DiscountDesription = discountDTO.Desription,
                DiscountPercentage = discountDTO.Percentage,
                Items = new List<Item>()
            };
            _webShop.Discounts.Create(discount);
        }

        public void DeleteDiscount(int id)
        {
            if (_webShop.Discounts.Get(id) != null)
            {
                _webShop.Discounts.Delete(id);
                _webShop.Save();
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
