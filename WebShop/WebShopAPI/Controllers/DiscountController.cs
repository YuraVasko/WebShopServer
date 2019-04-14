using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebShopBLL.Services;
using WebShopDAL.UnitOfWork;
using WebShopDto;

namespace WebShopAPI.Controllers
{
    public class DiscountController : ApiController
    {
        DiscountService _discountService;

        public DiscountController() : base()
        {
            ShopUnitOfWork shopUnitOfWork = new ShopUnitOfWork();
            _discountService = new DiscountService(shopUnitOfWork);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("all/discounts")]
        public HttpResponseMessage GetAllDiscounts()
        {
            var discounts = _discountService.GetAllDiscounts();
            return Request.CreateResponse<List<DiscountDTO>>(HttpStatusCode.OK, discounts);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("add/discount/per/item")]
        public HttpResponseMessage AddDiscountPerItem(AddDiscountToItemModel model)
        {
            _discountService.AddDiscountToItem(model.ItemId, model.DiscountId);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("add/new/discount")]
        public HttpResponseMessage AddNewDiscount([FromBody] DiscountDTO newDiscount)
        {
            _discountService.AddNewDiscount(newDiscount);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("delete/discount")]
        public HttpResponseMessage DeleteDiscount([FromBody] int discountId)
        {
            _discountService.DeleteDiscount(discountId);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("delete/item/discount")]
        public HttpResponseMessage DeleteItemDiscount([FromBody] int itemId)
        {
            _discountService.DeleteItemDiscount(itemId);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
