using CartingService.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartingService.BLL.Entities
{
    public class Cart : CartingService.BLL.Services.ICartService
    {
        private readonly ICartData _data;

        private DAL.Models.Cart cartDataModel;

        public Cart(ICartData data) => _data = data;

        public void Initialize(int id)
        {
            cartDataModel = _data.GetCartById(id);
        }

        public void Initialize()
        {
            DAL.Models.Cart newCart = new ();
            _data.AddCart(newCart);
            _data.Commit();
            cartDataModel = newCart;
        }

        public int Id 
        { 
            get
            {
                return cartDataModel.Id;
            }
        }

        public List<Item> Items
        {
            get
            {   if(cartDataModel.Items.Any())
                    return cartDataModel.Items.MapToBLEntity();
                else
                    return new List<Item>();
            }
        }

        public decimal TotalPrice 
        { 
            get 
            {
                decimal total = 0;
                if(Items.Count > 0)
                    total = Items.Sum(i => i.Price);
                return total;
            } 
        }

        public bool AddItemToCart(Item newItem)
        {
            cartDataModel.Items.Add(newItem.MapToDALModel());
            int firstCount = cartDataModel.Items.Count;
            _data.UpdateCart(cartDataModel);
            return _data.Commit() > firstCount; 
        }

        public IEnumerable<Item> GetListOfItems()
        {
            return Items;
        }

        public bool RemoveItemFromCart(int itemId)
        {
            var ItemRemoved = cartDataModel.Items.FirstOrDefault(i => i.Id == itemId);
            if (ItemRemoved != null)
            {
                cartDataModel.Items.Remove(ItemRemoved);
                _data.UpdateCart(cartDataModel);
            }
            return _data.Commit() > 0;
        }

        public int GetCartId()
        {
            return Id;
        }
    }
}
