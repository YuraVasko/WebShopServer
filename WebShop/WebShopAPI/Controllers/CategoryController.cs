using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebShopBLL.Services;
using WebShopDAL.UnitOfWork;
using WebShopDto;

namespace WebShopAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : ApiController
    {
        CategoryService categoryService;

        public CategoryController()
        {
            ShopUnitOfWork shopUnitOfWork = new ShopUnitOfWork();
            categoryService = new CategoryService(shopUnitOfWork);
        }

        [HttpPost]
        [Route("add/category")]
        public HttpResponseMessage AddNewCategory([FromBody] ItemCategoryDTO category)
        {
            categoryService.AddNewCategory(category);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("delete/category")]
        public HttpResponseMessage DeleteCategory([FromBody] string categoryName)
        {
            categoryService.DeleteCategoryByName(categoryName);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("add/category/to/item")]
        public HttpResponseMessage AddNewCategoryToItem([FromBody] int itemId, [FromBody] string categoryName)
        {
            categoryService.AddItemToCategory(itemId, categoryName);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("get/categories")]
        public HttpResponseMessage GetAllCategories()
        {
            var categories = categoryService.GetAllCategories();
            return Request.CreateResponse<List<ItemCategoryDTO>>(HttpStatusCode.OK, categories);
        }
    }
}
