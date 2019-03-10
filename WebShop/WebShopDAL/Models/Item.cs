using System.Collections.Generic;

namespace WebShopDAL.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Title { get; set; }
        public string Descripton { get; set; }
        public double Price { get; set; }
        public int AvailableCount { get; set; }
        public int? DiscountId { get; set; }
        public Discount Discount { get; set; }
        public ICollection<ItemCategory> Categories { get; set; }
        public ICollection<Basket> Baskets { get; set; }
        public ICollection<Purchase> Purchases { get; set; }
    }
}
