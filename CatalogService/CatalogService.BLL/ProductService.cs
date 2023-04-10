using CatalogService.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.BLL
{
    public class ProductService : IProductService
    {
        private readonly DAL.ICatalogData data;
        public ProductService(DAL.ICatalogData data)
        {
            this.data = data;
        }

        public Product Add(Product product)
        {
            var ProductAdded = data.AddProduct(product);
            data.Commit();
            return ProductAdded;
        }

        public async Task<Product> AddAsync(Product product)
        {
            var ProductAdded = data.AddProduct(product);
            await data.CommitAsync();
            return ProductAdded;
        }

        public bool Delete(int id)
        {
            var deleted = data.DeleteProduct(id);
            data.Commit();
            return deleted;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deleted = data.DeleteProduct(id);
            await data.CommitAsync();
            return deleted;
        }

        public Product Get(int id)
        {
            return data.GetProductById(id);
        }

        public async Task<Product> GetAsync(int id)
        {
            return await data.GetProductByIdAsync(id);
        }

        public IEnumerable<Product> List()
        {
            return data.GetAllProducts();
        }

        public async Task<List<Product>> ListAsync(int? categoryId, int pageNumber, int pageSize)
        {
            var allProducts = await data.GetAllProductsAsync();
            var query = allProducts.AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(i => i.CategoryId == categoryId.Value);
            }

            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public Product Update(Product product)
        {
            var ProductUpdated = data.UpdateProduct(product);
            data.Commit();
            return ProductUpdated;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            var ProductUpdated = data.UpdateProduct(product);
            await data.CommitAsync();
            return ProductUpdated;
        }
    }
}
