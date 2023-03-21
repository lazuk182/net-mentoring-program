using Microsoft.EntityFrameworkCore;

namespace CatalogService.Tests
{
    public class CatalogTests
    {
        private BLL.IProductService _productService;
        private BLL.ICategoryService _categoryService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DAL.CatalogContext>()
                .UseInMemoryDatabase(databaseName: "CatalogServiceDb")
                .Options;
            DAL.CatalogContext dbContext = new DAL.CatalogContext(options);
            DAL.ICatalogData catalogData = new DAL.CatalogDataEF(dbContext);
            _productService = new BLL.ProductService(catalogData);
            _categoryService = new BLL.CategoryService(catalogData);
        }

        [Test, Order(1)]
        public void AddCategoryTest()
        {
            var CategoryAdded = _categoryService.Add(new DAL.Models.Category 
            { 
                Image = "http://example.org/",
                Name = "Category One"
            });
            Assert.That(CategoryAdded.Id, Is.EqualTo(1));
        }

        [Test, Order(2)]
        public void AddMultipleCategoriesTest()
        {
            var FirstCategoryAdded = _categoryService.Add(new DAL.Models.Category
            {
                Image = "http://example.org/",
                Name = "Category Two"
            });
            Assert.That(FirstCategoryAdded.Id, Is.EqualTo(2));
            var SecondCategoryAdded = _categoryService.Add(new DAL.Models.Category
            {
                Image = "http://example.org/",
                Name = "Category Three"
            });
            Assert.That(SecondCategoryAdded.Id, Is.EqualTo(3));
        }

        [Test, Order(3)]
        public void ListCategoriesTest()
        {
            int NumberOfCategoriesAdded = _categoryService.List().Count();
            int NumberOfCategoriesExpected = 3;
            Assert.That(NumberOfCategoriesAdded, Is.EqualTo(NumberOfCategoriesExpected));
        }

        [Test, Order(4)]
        public void UpdateCategoryTest()
        {
            var FirstCategory = _categoryService.Get(1);
            FirstCategory.Name = "Category updated";
            _categoryService.Update(FirstCategory);
            FirstCategory = _categoryService.Get(1);
            Assert.That(FirstCategory.Name.Equals(FirstCategory.Name));
        }

        [Test, Order(5)]
        public void DeleteCategoryTest()
        {
            _categoryService.Add(new DAL.Models.Category
            {
                Image = "http://example.org/",
                Name = "Category to be deleted"
            });
            Assert.That(_categoryService.List().Count(), Is.EqualTo(4));
            _categoryService.Delete(4);
            Assert.That(_categoryService.List().Count(), Is.EqualTo(3));
        }

        [Test, Order(6)]
        public void AddProductTest()
        {
            var ProductAdded = _productService.Add(new DAL.Models.Product
            {
                Image = "http://example.org/",
                Name = "Product One",
                Amount = 1,
                CategoryId = 1,
                Description = "Description 1",
                Price = 1
            });
            Assert.That(ProductAdded.Id, Is.EqualTo(1));
        }

        [Test, Order(7)]
        public void AddMultipleProductsTest()
        {
            var FirstProductAdded = _productService.Add(new DAL.Models.Product
            {
                Image = "http://example.org/",
                Name = "Product Two",
                Amount = 1,
                CategoryId = 1,
                Description = "Description 1",
                Price = 1
            });
            Assert.That(FirstProductAdded.Id, Is.EqualTo(2));
            var SecondProductAdded = _productService.Add(new DAL.Models.Product
            {
                Image = "http://example.org/",
                Name = "Product Three",
                Amount = 1,
                CategoryId = 2,
                Description = "Description 1",
                Price = 1
            });
            Assert.That(SecondProductAdded.Id, Is.EqualTo(3));
        }

        [Test, Order(8)]
        public void ListProductsTest()
        {
            int NumberOfProductsAdded = _productService.List().Count();
            int NumberOfProductsExpected = 3;
            Assert.That(NumberOfProductsAdded, Is.EqualTo(NumberOfProductsExpected));
        }

        [Test, Order(9)]
        public void UpdateProductsTest()
        {
            var FirstProduct = _productService.Get(1);
            FirstProduct.Name = "Product updated";
            _productService.Update(FirstProduct);
            FirstProduct = _productService.Get(1);
            Assert.That(FirstProduct.Name.Equals(FirstProduct.Name));
        }

        [Test, Order(10)]
        public void DeleteProductTest()
        {
            _productService.Add(new DAL.Models.Product
            {
                Image = "http://example.org/",
                Name = "Product to be deleted",
                Amount = 1,
                CategoryId = 2,
                Description = "Description 1",
                Price = 1
            });
            Assert.That(_productService.List().Count(), Is.EqualTo(4));
            _productService.Delete(4);
            Assert.That(_productService.List().Count(), Is.EqualTo(3));
        }
    }
}