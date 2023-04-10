using CartingService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartingService.BLL.Services
{
    public class CartService : ICartService
    {
        private readonly DAL.ICartData data;
        public CartService(DAL.ICartData data) 
        {
            this.data = data;
        }

        public bool AddItemToCart(Item newItem, int cartId)
        {
            var cart = GetCartById(cartId);
            if (cart == null)
            {
                throw new ArgumentException("cartId was not found");
            }
            else
            {
                cart.Items.Add(newItem);
                data.UpdateCart(cart);
                data.Commit();
                return true;
            }
        }

        public Cart CreateCart()
        {
            var cart = new Cart();
            data.AddCart(cart);
            data.Commit();
            return cart;
        }

        public DAL.Models.Cart GetCartById(int Id)
        {
            var cart = data.GetCartById(Id);
            return cart;
        }

        public IEnumerable<DAL.Models.Item> GetListOfItemsFrom(int cartId)
        {
            var cart = GetCartById(cartId);
            if (cart == null)
            {
                throw new ArgumentException("cartId was not found");
            }
            else
            {
                return cart.Items;
            }
        }

        public bool RemoveItemFromCart(int itemId, int cartId)
        {
            var cart = GetCartById(cartId);
            if (cart == null)
            {
                throw new ArgumentException("cartId was not found");
            }
            else
            {
                var item = cart.Items.FirstOrDefault(i => i.Id == itemId);
                if(item == null)
                {
                    throw new ArgumentException("item was not found");
                }
                cart.Items.Remove(item);
                data.UpdateCart(cart);
                data.Commit();
                return true;
            }
        }
    }
}
