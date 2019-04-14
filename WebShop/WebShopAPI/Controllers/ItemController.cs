using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebShopBLL.Services;
using WebShopDAL.UnitOfWork;
using WebShopDto;

namespace WebShopAPI.Controllers
{
    public class ItemController : ApiController
    {
        ItemService itemService;

        public ItemController()
        {
            ShopUnitOfWork shopUnitOfWork = new ShopUnitOfWork();
            itemService = new ItemService(shopUnitOfWork);
        }

        [HttpGet]
        [Route("get/items/page/{pageNumber:int}")]
        public HttpResponseMessage GetAllItems(int pageNumber)
        {
            var allItems = itemService.GetItemsFromPage(pageNumber);
            return Request.CreateResponse<IEnumerable<ItemDTO>>(HttpStatusCode.OK, allItems);
        }

        [HttpGet]
        [Route("get/items/{category}")]
        public HttpResponseMessage GetItemsByCategory([FromUri]string category)
        {
            var allItems = itemService.GetAllItemsByCategory(category);
            return Request.CreateResponse<IEnumerable<ItemDTO>>(HttpStatusCode.OK, allItems);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("add/item")]
        public HttpResponseMessage AddNewItem([FromBody]ItemDTO newItem)
        {
            itemService.AddNewItem(newItem);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("delete/item")]
        public HttpResponseMessage DeleteItem([FromBody]int itemId)
        {
            itemService.DeleteItemById(itemId);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("item/{id}")]
        public HttpResponseMessage GetItemDetails([FromUri]int id)
        {
            return Request.CreateResponse<ItemDTO>(HttpStatusCode.OK, itemService.GetItemDetails(id));
        }
    }
}
