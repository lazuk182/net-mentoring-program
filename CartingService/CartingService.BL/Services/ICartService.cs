using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartingService.BLL.Services
{
    public interface ICartService
    {
        void Initialize(int id);

        void Initialize();

        int GetCartId();

        IEnumerable<Entities.Item> GetListOfItems();

        bool AddItemToCart(Entities.Item newItem);

        bool RemoveItemFromCart(int itemId);
    }
}
