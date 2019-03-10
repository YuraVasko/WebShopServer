using System.Collections.Generic;
namespace WebShopDAL.Models
{
    public class Basket
    {
        public int BasketId { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
