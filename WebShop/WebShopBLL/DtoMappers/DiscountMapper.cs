using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDAL.Models;
using WebShopDto;

namespace WebShopBLL.DtoMappers
{
    class DiscountMapper
    {
        public Discount GetDiscountModelDiscountDTO(DiscountDTO discount)
        {
            return new Discount
            {
                DiscountId = discount.Id,
                DiscountDesription = discount.Desription,
                DiscountPercentage = discount.Percentage
            };
        }

        public DiscountDTO GetDiscountDTOFromDiscountModel(Discount discount)
        {
            return new DiscountDTO
            {
                Desription = discount.DiscountDesription,
                Id = discount.DiscountId,
                Percentage = discount.DiscountPercentage
            };
        }
    }
}
