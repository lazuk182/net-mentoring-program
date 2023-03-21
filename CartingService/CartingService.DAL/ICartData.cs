using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartingService.DAL
{
    public interface ICartData
    {
        int Commit();
        Models.Cart AddCart(Models.Cart newCart);

        int UpdateCart(Models.Cart updatedCart);

        int DeleteCart(int cartId);

        Models.Cart GetCartById(int cartId);

        IEnumerable<Models.Cart> GetAll();

        int GetMaxId();
    }
}
