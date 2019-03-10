using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDAL.Models;

namespace WebShopDAL.EntityFramework
{
    class CustomInitializer : DropCreateDatabaseAlways<Context>
    {
        protected override void Seed(Context db)
        {
            db.UserRoles.AddRange(new List<UserRole>
            {
                new UserRole { UserRoleName = "Client"},
                new UserRole { UserRoleName = "GlobalAdmin"},
                new UserRole { UserRoleName = "ShopAdministrator"},
            });

            db.UserStatuses.AddRange(new List<UserStatus>
            {
                new UserStatus { UserStatusName = "Active", UserStatusDescription = "Active user"},
                new UserStatus { UserStatusName = "Blocked", UserStatusDescription = "User who was banned due to some conditions"},
                new UserStatus { UserStatusName = "WaitConfirmation", UserStatusDescription = "User who has registred but do not confirm email"},
            });
            db.SaveChanges();
        }
    }
}
