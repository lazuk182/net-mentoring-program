using CartingService.BLL.Services;
using CartingService.DAL.Database;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CartingService.Test
{
    public class CartingServiceMemoryDb
    {
        private ICartService cartService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<CartingContext>()
                .UseInMemoryDatabase(databaseName: "cartingDb")
                .Options;
            CartingContext dbContext = new CartingContext(options);
            DAL.ICartData cartData = new DAL.CartData(dbContext);
            cartService = new BLL.Entities.Cart(cartData);
        }

        [Test, Order(1)]
        public void CreateCart()
        {
            cartService.Initialize();
            Assert.AreEqual(1, cartService.GetCartId());
        }

        [Test, Order(2)]
        public void AddItemToCart()
        {
            cartService.Initialize();
            var newItem = new BLL.Entities.Item()
            {
                Image = "http://image.url",
                Name = "Test Item",
                Price = 99,
                Quantity = 1
            };
            cartService.AddItemToCart(newItem);
            Assert.AreEqual(1, cartService.GetListOfItems().Count());
        }

        [Test, Order(3)]
        public void AddMultipleItemsToCart()
        {
            cartService.Initialize();
            var listOfItems = new List<BLL.Entities.Item>
            {
                new BLL.Entities.Item
                {
                    Image = "http://image.url",
                    Name = "Test Item 1",
                    Price = 99,
                    Quantity = 1
                },
                new BLL.Entities.Item
                {
                    Image = "http://image.url",
                    Name = "Test Item 2",
                    Price = 30,
                    Quantity = 1
                },
                new BLL.Entities.Item
                {
                    Image = "http://image.url",
                    Name = "Test Item 3",
                    Price = 19,
                    Quantity = 1
                },
                new BLL.Entities.Item
                {
                    Image = "http://image.url",
                    Name = "Test Item 4",
                    Price = Convert.ToDecimal(41.30),
                    Quantity = 1
                },
            };
            foreach(var newItem in listOfItems)
            {
                cartService.AddItemToCart(newItem);
            }
            Assert.AreEqual(listOfItems.Count(), cartService.GetListOfItems().Count());
        }

        [Test, Order(4)]
        public void RemoveItemFromCart()
        {
            cartService.Initialize();
            var listOfItems = new List<BLL.Entities.Item>
            {
                new BLL.Entities.Item
                {
                    Image = "http://image.url",
                    Name = "Test Item 1",
                    Price = 99,
                    Quantity = 1
                },
                new BLL.Entities.Item
                {
                    Image = "http://image.url",
                    Name = "Test Item 2",
                    Price = 30,
                    Quantity = 1
                }
            };
            foreach (var newItem in listOfItems)
            {
                cartService.AddItemToCart(newItem);
            }
            var ItemsInCart = cartService.GetListOfItems();
            var ItemToBeDeletedId = ItemsInCart.First().Id;
            cartService.RemoveItemFromCart(ItemToBeDeletedId);
            Assert.AreEqual(1, cartService.GetListOfItems().Count());
        }

        [Test, Order(5)]
        public void ListElementsOfCart()
        {
            cartService.Initialize();
            var listOfItems = new List<BLL.Entities.Item>
            {
                new BLL.Entities.Item
                {
                    Image = "http://image.url",
                    Name = "Test Item 1",
                    Price = 99,
                    Quantity = 1
                },
                new BLL.Entities.Item
                {
                    Image = "http://image.url",
                    Name = "Test Item 2",
                    Price = 30,
                    Quantity = 1
                }
            };
            foreach (var newItem in listOfItems)
            {
                cartService.AddItemToCart(newItem);
            }
            Assert.AreEqual(listOfItems.Count(), cartService.GetListOfItems().Count());            
        }
    }
}