using System.Collections.Generic;
using WebShopDto;

namespace WebShopBLL.Interfaces
{
    interface IDiscountService
    {
        IEnumerable<DiscountDTO> GetAllDiscounts();
        void AddDiscount(DiscountDTO discountDTO);
        void DeleteDiscount(int id);
    }
}
