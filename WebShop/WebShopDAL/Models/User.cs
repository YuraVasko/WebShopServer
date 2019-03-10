using System;
using System.Collections.Generic;

namespace WebShopDAL.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserEmailLogin { get; set; }
        public string UserPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthdayDate { get; set; }
        public int UserRoleId { get; set; }
        public UserRole UserRole { get; set; }
        public int? BasketId { get; set; }
        public Basket Basket { get; set; }
        public int UserStatusId { get; set; }
        public UserStatus UserStatus { get; set; }
        public ICollection<Purchase> Purchases { get; set; }
    }
}
