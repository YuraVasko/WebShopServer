using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDAL.Models;

namespace WebShopDAL.EntityFramework
{
    public class UserStore : UserStore<User>
    {
        public UserStore() : base(new Context())
        {
        }
    }
}
