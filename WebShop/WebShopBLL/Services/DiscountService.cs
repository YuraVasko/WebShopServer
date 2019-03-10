using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopBLL.DtoMappers;
using WebShopDAL.Interfaces;
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
            var result = new List<DiscountDTO>();
            _webShop.Discounts.GetAll().ToList().ForEach(d =>
            {
                result.Add(_discountMapper.GetDiscountDTOFromDiscountModel(d));
            });
            return result;
        }

        public void AddDiscount(DiscountDTO discountDTO)
        {
            var discount = _discountMapper.GetDiscountModelDiscountDTO(discountDTO);
            _webShop.Discounts.Create(discount);
            _webShop.Save();
        }

        public void UpdateCategory(DiscountDTO discountDTO)
        {
            var discount = _discountMapper.GetDiscountModelDiscountDTO(discountDTO);
            _webShop.Discounts.Update(discount);
            _webShop.Save();
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
