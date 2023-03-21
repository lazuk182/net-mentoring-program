using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartingService.BLL
{
    public static class Extensions
    {
        public static DAL.Models.Cart MapToDALModel(this Entities.Cart cart)
        {
            List<DAL.Models.Item> Items = new ();
            foreach (var item in cart.Items)
            {
                Items.Add(new DAL.Models.Item
                {
                   Id = item.Id,
                   Name = item.Name,
                   Price = item.Price,
                   Image = item.Image,
                   Quantity = item.Quantity
                });
            }
            return new DAL.Models.Cart
            {
                Id = cart.Id,
                Items = Items
            };
        }

        public static List<Entities.Item> MapToBLEntity(this IEnumerable<DAL.Models.Item> cartItems)
        {
            List<Entities.Item> Items = new ();
            foreach (var item in cartItems)
            {
                Items.Add(new Entities.Item
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Image = item.Image,
                    Quantity = item.Quantity
                });
            }
            return Items;
        }

        public static DAL.Models.Item MapToDALModel(this Entities.Item item)
        {
            return new DAL.Models.Item
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                Image = item.Image,
                Quantity = item.Quantity
            };
        }
    }
}
