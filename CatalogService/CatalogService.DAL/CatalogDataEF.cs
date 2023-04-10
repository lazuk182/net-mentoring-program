using CatalogService.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.DAL
{
    public class CatalogDataEF : ICatalogData
    {
        private readonly CatalogContext db;
        public CatalogDataEF(CatalogContext catalogContext)
        {
            db = catalogContext;
        }
        public Category AddCategory(Category category)
        {
            var entity = db.Attach(category);
            entity.State = Microsoft.EntityFrameworkCore.EntityState.Added;
            return category;
        }

        public Product AddProduct(Product product)
        {
            var entity = db.Attach(product);
            entity.State = Microsoft.EntityFrameworkCore.EntityState.Added;
            return product;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await db.SaveChangesAsync();
        }

        public bool DeleteCategory(int id)
        {
            var CategoryDeleted = GetCategoryById(id);
            db.Categories.Remove(CategoryDeleted);
            return true;
        }

        public bool DeleteProduct(int id)
        {
            var ProductDeleted = GetProductById(id);
            db.Products.Remove(ProductDeleted);
            return true;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return db.Categories;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await db.Categories.ToListAsync();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return db.Products;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await db.Products.ToListAsync();
        }

        public Category GetCategoryById(int id)
        {
            return db.Categories.Find(id);
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await db.Categories.FindAsync(id);
        }

        public Product GetProductById(int id)
        {
            return db.Products.Find(id);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await db.Products.FindAsync(id);
        }

        public Category UpdateCategory(Category category)
        {
            var entity = db.Attach(category);
            entity.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return category;
        }

        public Product UpdateProduct(Product product)
        {
            var entity = db.Attach(product);
            entity.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return product;
        }
    }
}
