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
        private ICartService? cartService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<CartingContext>()
                .UseInMemoryDatabase(databaseName: "cartingDb")
                .Options;
            CartingContext dbContext = new CartingContext(options);
            DAL.ICartData cartData = new DAL.CartData(dbContext);
            cartService = new CartService(cartData);
        }

        [Test, Order(1)]
        public void CreateCart()
        {
            var newCart = cartService.CreateCart();
            Assert.AreEqual(1, newCart.Id);
        }

        [Test, Order(2)]
        public void AddItemToCart()
        {
            var newCart = cartService.CreateCart();
            var newItem = new DAL.Models.Item()
            {
                Image = "http://image.url",
                Name = "Test Item",
                Price = 99,
                Quantity = 1
            };
            cartService.AddItemToCart(newItem, newCart.Id);
            Assert.AreEqual(1, newCart.Items.Count());
        }

        [Test, Order(3)]
        public void AddMultipleItemsToCart()
        {
            var newCart = cartService.CreateCart();
            var listOfItems = new List<DAL.Models.Item>
            {
                new DAL.Models.Item
                {
                    Image = "http://image.url",
                    Name = "Test Item 1",
                    Price = 99,
                    Quantity = 1
                },
                new DAL.Models.Item
                {
                    Image = "http://image.url",
                    Name = "Test Item 2",
                    Price = 30,
                    Quantity = 1
                },
                new DAL.Models.Item
                {
                    Image = "http://image.url",
                    Name = "Test Item 3",
                    Price = 19,
                    Quantity = 1
                },
                new DAL.Models.Item
                {
                    Image = "http://image.url",
                    Name = "Test Item 4",
                    Price = Convert.ToDecimal(41.30),
                    Quantity = 1
                },
            };
            foreach(var newItem in listOfItems)
            {
                cartService.AddItemToCart(newItem, newCart.Id);
            }
            Assert.AreEqual(listOfItems.Count(), newCart.Items.Count());
        }

        [Test, Order(4)]
        public void RemoveItemFromCart()
        {
            var newCart = cartService.CreateCart();
            var listOfItems = new List<DAL.Models.Item>
            {
                new DAL.Models.Item
                {
                    Image = "http://image.url",
                    Name = "Test Item 1",
                    Price = 99,
                    Quantity = 1
                },
                new DAL.Models.Item
                {
                    Image = "http://image.url",
                    Name = "Test Item 2",
                    Price = 30,
                    Quantity = 1
                }
            };
            foreach (var newItem in listOfItems)
            {
                cartService.AddItemToCart(newItem, newCart.Id);
            }
            var ItemsInCart = newCart.Items;
            var ItemToBeDeletedId = ItemsInCart.First().Id;
            cartService.RemoveItemFromCart(ItemToBeDeletedId, newCart.Id);
            Assert.AreEqual(1, newCart.Items.Count());
        }

        [Test, Order(5)]
        public void ListElementsOfCart()
        {
            var newCart = cartService.CreateCart();
            var listOfItems = new List<DAL.Models.Item>
            {
                new DAL.Models.Item
                {
                    Image = "http://image.url",
                    Name = "Test Item 1",
                    Price = 99,
                    Quantity = 1
                },
                new DAL.Models.Item
                {
                    Image = "http://image.url",
                    Name = "Test Item 2",
                    Price = 30,
                    Quantity = 1
                }
            };
            foreach (var newItem in listOfItems)
            {
                cartService.AddItemToCart(newItem, newCart.Id);
            }
            Assert.AreEqual(listOfItems.Count(), newCart.Items.Count());            
        }
    }
}