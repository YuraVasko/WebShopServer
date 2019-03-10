using System.Collections.Generic;

namespace WebShopDAL.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
