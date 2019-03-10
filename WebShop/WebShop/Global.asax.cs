using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebShopBLL;
using WebShopDAL.EntityFramework;
using WebShopDAL.Models;
using WebShopDto.User;
using WebShopDAL.UnitOfWork;

namespace WebShop
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            UserRegistrationDTO user1 = new UserRegistrationDTO
            {
                Login = "a@123",
                Password = "123",
                FirstName= "Yura",
                LastName= "Vasko",
                BirthdayDate = DateTime.Now
            };
            UserRegistrationDTO user2 = new UserRegistrationDTO
            {
                Login = "a@123",
                Password = "123",
                FirstName = "Nazar",
                LastName = "Morozyuk",
                BirthdayDate = DateTime.Now
            };
            using (Context db = new Context())
            {
                UserService service = new UserService(new ShopUnitOfWork());
                service.AddNewClient(user1);
                service.DeleteUserById(2);

                db.SaveChanges();
            }
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
