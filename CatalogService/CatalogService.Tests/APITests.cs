using CatalogService.API.Controllers;
using CatalogService.BLL;
using CatalogService.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Tests
{
    [TestFixture]
    public class APITests
    {
        private CatalogController _controller;
        private Mock<IProductService> _itemRepositoryMock;
        private Mock<ICategoryService> _categoryRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _itemRepositoryMock = new Mock<IProductService>();
            _categoryRepositoryMock = new Mock<ICategoryService>();
            _controller = new CatalogController(_itemRepositoryMock.Object, _categoryRepositoryMock.Object);
        }

        [Test]
        public async Task GetAllItems_ShouldReturnAllItems()
        {
            // Arrange
            var expectedItems = new List<Product>
        {
            new Product { Id = 1, Name = "Item 1", CategoryId = 1 },
            new Product { Id = 2, Name = "Item 2", CategoryId = 1 },
            new Product { Id = 3, Name = "Item 3", CategoryId = 2 }
        };
            _itemRepositoryMock.Setup(repo => repo.ListAsync(null, 1, 10))
                .ReturnsAsync(expectedItems);

            // Act
            var result = await _controller.GetItems(null, 1, 10);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = (OkObjectResult)result.Result;
            Assert.IsInstanceOf<List<Product>>(okResult.Value);
            var items = (List<Product>)okResult.Value;
            Assert.That(items.Count, Is.EqualTo(expectedItems.Count));
        }

        [Test]
        public async Task GetItemsByCategoryId_ReturnsOkResponse()
        {
            // Arrange
            int categoryId = 1;
            var items = new List<Product> { new Product { Id = 1, Name = "Item 1", CategoryId = categoryId }, 
                new Product { Id = 2, Name = "Item 2", CategoryId = categoryId } };
            _itemRepositoryMock.Setup(repo => repo.ListAsync(categoryId, 1, 10)).ReturnsAsync(items);

            // Act
            var result = await _controller.GetItems(categoryId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = (OkObjectResult)result.Result;
            Assert.AreEqual(200, okResult.StatusCode);

            var model = okResult.Value as List<Product>;
            Assert.IsNotNull(model);
            Assert.AreEqual(items.Count, model.Count);
            Assert.AreEqual(items[0].Name, model[0].Name);
            Assert.AreEqual(items[1].Name, model[1].Name);
        }

        [Test]
        public async Task GetItemsByCategoryId_ReturnsNotFoundResponse_WhenCategoryNotFound()
        {
            // Arrange
            int categoryId = 1;
            _itemRepositoryMock.Setup(repo => repo.ListAsync(categoryId, 1, 10)).ReturnsAsync((List<Product>)null);

            // Act
            var result = await _controller.GetItems(categoryId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
            var notFoundResult = (NotFoundResult)result.Result;
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [Test]
        public async Task AddCategory_ReturnsCreatedResponse()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Category 1" };
            _categoryRepositoryMock.Setup(repo => repo.AddAsync(category)).ReturnsAsync(category);

            // Act
            var result = await _controller.AddCategory(category);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
            var createdResult = (CreatedAtActionResult)result.Result;
            Assert.AreEqual(201, createdResult.StatusCode);

            var model = createdResult.Value as Category;
            Assert.IsNotNull(model);
            Assert.AreEqual(category.Id, model.Id);
            Assert.AreEqual(category.Name, model.Name);
        }

        [Test]
        public async Task AddItem_ReturnsCreatedResponse()
        {
            // Arrange
            var item = new Product { Id = 1, Name = "Item 1", CategoryId = 1 };
            object value = _itemRepositoryMock.Setup(repo => repo.AddAsync(item)).ReturnsAsync(item);

            // Act
            var result = await _controller.AddItem(item);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
            var createdResult = (CreatedAtActionResult)result.Result;
            Assert.AreEqual(201, createdResult.StatusCode);

            var model = createdResult.Value as Product;
            Assert.IsNotNull(model);
            Assert.AreEqual(item.Id, model.Id);
            Assert.AreEqual(item.Name, model.Name);
            Assert.AreEqual(item.CategoryId, model.CategoryId);
        }
    }
}
