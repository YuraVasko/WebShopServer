using System.Collections.Generic;

namespace WebShopDto
{
    public class DiscountDTO
    {
        public int Id { get; set; }
        public double Percentage { get; set; }
        public string Desription { get; set; }
        public IEnumerable<ItemDTO> Items { get; set; }
    }
}
