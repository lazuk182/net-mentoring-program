using CartingService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartingService.BLL.Services
{
    public interface ICartService
    {
        DAL.Models.Cart GetCartById(int Id);

        IEnumerable<DAL.Models.Item> GetListOfItemsFrom(int cartId);

        bool AddItemToCart(DAL.Models.Item newItem, int cartId);

        bool RemoveItemFromCart(int itemId, int cartId);

        DAL.Models.Cart CreateCart();

        DAL.Models.Item UpdateItemInformation(Item item);
    }
}
