using CartingService.DAL.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartingService.DAL
{
    public class CartLiteDb : ICartData
    {
        private readonly LiteDB.LiteDatabase db;
        public CartLiteDb(LiteDB.LiteDatabase db)
        {
            this.db = db;
            if (!db.CollectionExists("Items"))
            {
                var itemsCollection = db.GetCollection<Cart>("items");
                itemsCollection.EnsureIndex(x => x.Id, true);
            }

            if (!db.CollectionExists("Carts"))
            {
                var cartsCollection = db.GetCollection<Cart>("Carts");
                cartsCollection.EnsureIndex(x => x.Id, true);
            }
            
            var mapper = BsonMapper.Global;
            mapper.Entity<Cart>()
                .DbRef(x => x.Items, "Items");
            mapper.Entity<Item>()
                .DbRef(x => x.Cart, "Carts");
        }

        public Cart AddCart(Cart newCart)
        {
            var col = db.GetCollection<Cart>("Carts");
            var result = col.Insert(newCart);
            //var items = db.GetCollection<Item>("Items");
            //items.InsertBulk(newCart.Items);
            newCart.Id = result.AsInt32;
            return newCart;
        }

        public int Commit()
        {
            return 1;
        }

        public int DeleteCart(int cartId)
        {
            var col = db.GetCollection<Cart>("Carts");
            col.DeleteMany(x => x.Id == cartId);            
            return 1;
        }

        public IEnumerable<Cart> GetAll()
        {
            var col = db.GetCollection<Cart>("Carts");
            return col.FindAll();
        }

        public Cart GetCartById(int cartId)
        {
            var col = db.GetCollection<Cart>("Carts");
            return col.Include(x => x.Items).Find(x => x.Id == cartId).FirstOrDefault();
        }

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public int UpdateCart(Cart updatedCart)
        {
            var carts = db.GetCollection<Cart>("Carts");
            carts.Update(updatedCart);
            var items = db.GetCollection<Item>("Items");
            foreach(var item in updatedCart.Items)
                items.Update(item);
            return 1;
        }
    }
}
