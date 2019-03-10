using System.Collections.Generic;

namespace WebShopDto
{
    public class ItemDTO
    {
        public int ItemId { get; set; }
        public string Title { get; set; }
        public string Descripton { get; set; }
        public double Price { get; set; }
        public int AvailableCount { get; set; }
        public DiscountDTO Discount { get; set; }
        public ICollection<ItemCategoryDTO> Categories { get; set; }
    }
}
