using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace WebShopDAL.Models
{
    public class User : IdentityUser
    {
        public int? BasketId { get; set; }
        public Basket Basket { get; set; }
        public int? UserStatusId { get; set; }
        public UserStatus UserStatus { get; set; }
        public ICollection<Purchase> Purchases { get; set; }
    }
}
