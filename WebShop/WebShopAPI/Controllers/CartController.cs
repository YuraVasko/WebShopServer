using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebShopAPI.Identity;
using WebShopBLL.Services;
using WebShopDAL.UnitOfWork;
using WebShopDto;

namespace WebShopAPI.Controllers
{
    public class CartController : ApiController
    {
        CustomJWTFormat _jwtFormat;
        CartService _cartService;

        public CartController() : base()
        {
            ShopUnitOfWork shopUnitOfWork = new ShopUnitOfWork();
            _cartService = new CartService(shopUnitOfWork);
            _jwtFormat = new CustomJWTFormat();


        }

        [HttpGet]
        [Authorize]
        [Route("user/cart/{userName}/page/{pageNumber:int}")]
        public HttpResponseMessage GetUserCart(string userName, int pageNumber)
        {
            string currentUser = GetCurrentUserName();
            if( userName == currentUser )
            {
                var userCart = _cartService.GetUserCart(userName, pageNumber);
                return Request.CreateResponse<List<ItemDTO>>(HttpStatusCode.OK, userCart);
            }
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }

        [HttpGet]
        [Authorize]
        [Route("user/purchase/{userName}/page/{pageNumber:int}")]
        public HttpResponseMessage GetUserPurchase(string userName, int pageNumber)
        {
            string currentUser = GetCurrentUserName();
            if (userName == currentUser)
            {
                var userCart = _cartService.GetUserPurchase(userName, pageNumber);
                return Request.CreateResponse<List<ItemDTO>>(HttpStatusCode.OK, userCart);
            }
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }

        [HttpPost]
        [Authorize]
        [Route("submit/purchase")]
        public HttpResponseMessage SubmitPurchase([FromBody]ItemUserIdDto model)
        {
            string currentUser = GetCurrentUserName();
            if (model.UserId == currentUser)
            {
                _cartService.SubmitUserPurchase(model.UserId);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }

        [HttpPost]
        [Authorize]
        [Route("clear/cart")]
        public HttpResponseMessage ClearCart([FromBody]ItemUserIdDto model)
        {
            string currentUser = GetCurrentUserName();
            if (model.UserId == currentUser)
            {
                _cartService.ClearCart(model.UserId);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }

        [HttpPost]
        [Authorize]
        [Route("add/item/in/cart")]
        public HttpResponseMessage AddItemToCart([FromBody]ItemUserIdDto model)
        {
            string currentUser = GetCurrentUserName();
            if (model.UserId == currentUser)
            {
                _cartService.AddItemToCart(model.UserId, model.ItemId);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }

        [HttpPost]
        [Authorize]
        [Route("delete/item/from/cart")]
        public HttpResponseMessage DeleteItemFromCart([FromBody] ItemUserIdDto model)
        {
            string currentUser = GetCurrentUserName();
            if (model.UserId == currentUser)
            {
                _cartService.DeleteItemFromCart(model.UserId, model.ItemId);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }

        private string GetCurrentUserName()
        {
            string encoded = Request.Headers.Authorization.Parameter;
            object currentUserName;
            _jwtFormat.GetDataFromEncodedTocken(encoded).Payload.TryGetValue("id", out currentUserName);
            return currentUserName as string;
        }

    }
}
