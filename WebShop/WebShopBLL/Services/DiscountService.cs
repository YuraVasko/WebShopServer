using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopBLL.DtoMappers;
using WebShopDAL.Interfaces;
using WebShopDAL.Models;
using WebShopDto;

namespace WebShopBLL.Services
{
    class DiscountService
    {
        private IUnitOfWork _webShop;
        private DiscountMapper _discountMapper;

        public DiscountService(IUnitOfWork webShop)
        {
            _webShop = webShop;
            _discountMapper = new DiscountMapper();
        }

        public IEnumerable<DiscountDTO> GetAllDiscounts()
        {
            return _webShop.Discounts.GetAll().Select(d => new DiscountDTO
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

        public void UpdateCategory(DiscountDTO discountDTO)
        {
            var discount = new Discount
            {
                DiscountId = discountDTO.Id,
                DiscountDesription = discountDTO.Desription,
                DiscountPercentage = discountDTO.Percentage,
                Items 
            };
            _webShop.Discounts.Update(discount);
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
