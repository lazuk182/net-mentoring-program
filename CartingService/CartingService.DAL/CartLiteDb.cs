using CartingService.DAL.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CartingService.DAL
{
    public class CartLiteDb : ICartData
    {
        private LiteDatabase db;
        private ILiteCollection<Cart> cartsCollection;
        private ILiteCollection<Item> itemsCollection;

        public CartLiteDb(LiteDatabase db)
        {
            this.db = db;
            cartsCollection = db.GetCollection<Cart>("Carts");
            itemsCollection = db.GetCollection<Item>("Items");

            if (!db.CollectionExists("Items"))
            {
                itemsCollection.EnsureIndex(x => x.Id, true);
            }            

            if (!db.CollectionExists("Carts"))
                cartsCollection.EnsureIndex(x => x.Id, true);
        }

        public Cart AddCart(Cart newCart)
        {
            cartsCollection.Insert(newCart);
            if (newCart.Items.Count > 0)
            {
                itemsCollection.InsertBulk(newCart.Items);
            }
            return newCart;
        }

        public int UpdateCart(Cart updatedCart)
        {
            bool WasUpdated = true;
            foreach (var item in updatedCart.Items)
            {
                WasUpdated &= itemsCollection.Upsert(item);
            }
            bool wasUpdated = cartsCollection.Update(updatedCart);
            return wasUpdated ? 1 : 0;
        }

        public int Commit()
        {
            return db.Commit() ? 1 : 0;
        }

        public int DeleteCart(int cartId)
        {
            var col = db.GetCollection<Cart>("Carts");
            col.DeleteMany(x => x.Id == cartId);            
            return 1;
        }

        public IEnumerable<Cart> GetAll()
        {
            return cartsCollection.Include(cart => cart.Items).FindAll();
        }

        public Cart GetCartById(int cartId)
        {
            return cartsCollection.Include(cart => cart.Items).FindById(cartId);
        }

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public Item UpdateItem(Item item)
        {
            itemsCollection.Update(item);
            return item;
        }
    }
}
