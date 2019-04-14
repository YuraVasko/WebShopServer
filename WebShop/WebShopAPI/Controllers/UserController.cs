using System;
using System.Net;
using System.Net.Http;
using WebShopDAL.UnitOfWork;
using System.Web.Http;
using WebShopDto.User;
using WebShopBLL.Services;
using WebShopAPI.Identity;

namespace WebShopAPI.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        ShopUnitOfWork shopUnitOfWork;
        UserService userService;
        CustomJWTFormat formatter;

        public UserController() : base()
        {
            shopUnitOfWork = new ShopUnitOfWork();
            userService = new UserService(shopUnitOfWork);
            formatter = new CustomJWTFormat();
        }

        [HttpPost]
        [Route("user/register/external")]
        [Authorize(Roles = "Admin")]
        public HttpResponseMessage RegisterUserWithSpecifiedRole([FromBody]UserDTO userRegistration)
        {
            try
            {
                userService.AddNewUser(userRegistration, userRegistration.RoleName);
            }
            catch (ArgumentException)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("user/register")]
        [AllowAnonymous]
        public HttpResponseMessage RegisterUser([FromBody]UserDTO userRegistration)
        {
            try
            {
                userService.AddNewUser(userRegistration, "User");
            }
            catch (ArgumentException)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("user/edit")]
        public HttpResponseMessage EditUserData([FromBody]UserDTO userRegistration)
        {
            string currentUserName = GetCurrentUserName();
            if (!userService.HasUserPermission(currentUserName as string, userRegistration.Login))
            {
                return new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
            try
            {

            }
            catch (ArgumentException)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("user/delete")]
        [Authorize(Roles = "Admin")]
        public HttpResponseMessage DeleteUser(string userName)
        {
            try
            {
                userService.DeleteUser(userName);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (ArgumentException)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        private string GetCurrentUserName()
        {
            string encoded = Request.Headers.Authorization.Parameter;
            object currentUserName;
            formatter.GetDataFromEncodedTocken(encoded).Payload.TryGetValue("userName", out currentUserName);
            return currentUserName as string ;
        }
    }
}
