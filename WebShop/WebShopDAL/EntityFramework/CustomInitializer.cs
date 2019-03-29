using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDAL.EntityFramework
{
    class CustomInitializer : DropCreateDatabaseAlways<Context>
    {
        protected override void Seed(Context context)
        {
            //default roles
            context.Roles.Add(new IdentityRole("Admin"));
            context.Roles.Add(new IdentityRole("User"));
        }
    }
}
