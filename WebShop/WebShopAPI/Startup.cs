using Microsoft.Owin;
using WebShopAPI.App_Start;

[assembly: OwinStartup(typeof(Startup))]

namespace WebShopAPI.App_Start
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);
        }
    }
}
