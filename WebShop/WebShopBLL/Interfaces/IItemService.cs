using System.Collections.Generic;
using WebShopDto;

namespace WebShopBLL.Interfaces
{
    interface IItemService
    {
        IEnumerable<ItemDTO> GetAllItems();
        void AddNewItem(ItemDTO newItem);
        void DeleteItem(int id);
    }
}
