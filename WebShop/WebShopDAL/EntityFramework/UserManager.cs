using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDAL.Models;

namespace WebShopDAL.EntityFramework
{
    public class UserManager : UserManager<User>
    {
        public UserManager() : base(new UserStore())
        {
        }
    }
}
