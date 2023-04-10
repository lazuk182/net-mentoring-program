using CartingService.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartingService.DAL
{
    public class CartData : ICartData
    {
        private readonly Database.CartingContext _context;
        public CartData(Database.CartingContext context)
        {
            _context = context;
        }

        public Cart AddCart(Cart newCart)
        {
            var entity = _context.Carts.Attach(newCart);
            entity.State = EntityState.Added;
            return newCart;
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public int DeleteCart(int cartId)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.Id == cartId);
            if(cart != null)
                _context.Carts.Remove(cart);

            return _context.Carts.Count();
        }

        public IEnumerable<Cart> GetAll()
        {
            return _context.Carts;
        }

        public Cart GetCartById(int cartId)
        {
            return _context.Carts.Include(c => c.Items).FirstOrDefault(c => c.Id == cartId);
        }

        public int GetMaxId()
        {
            if (_context.Carts.Any())
            {
                return _context.Carts.Max(e => e.Id);
            }
            return 0;
        }

        public int UpdateCart(Cart updatedCart)
        {
            var entity = _context.Carts.Attach(updatedCart);
            entity.State = EntityState.Modified;
            return 1;
        }
    }
}
