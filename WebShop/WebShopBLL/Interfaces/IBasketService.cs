using System.Collections.Generic;
using WebShopDto;

namespace WebShopBLL.Interfaces
{
    interface IBasketService
    {
        void AddNewItemInUserBasket(int basketId, int itemId);
        void RemoveItemFromUserBasket(int basketId, int itemId);
        void ClearBasket(int basketId);
        IEnumerable<ItemDTO> GetItemsFromBasket(int basketId);
    }
}
