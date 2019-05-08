using Microsoft.Owin;
using WebShopAPI.App_Start;

[assembly: OwinStartup(typeof(Startup))]

namespace WebShopAPI.App_Start
{
    using Owin;
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using WebShopAPI.SecondTask;

    public partial class Startup
    {
        private static readonly HttpClient client = new HttpClient();

        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);
        }
    }
}
