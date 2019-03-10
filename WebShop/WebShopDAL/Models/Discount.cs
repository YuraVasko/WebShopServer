using System.Collections.Generic;

namespace WebShopDAL.Models
{
    public class Discount
    {
        public int DiscountId { get; set; }
        public double DiscountPercentage { get; set; }
        public string DiscountDesription { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
