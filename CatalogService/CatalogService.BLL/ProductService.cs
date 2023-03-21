using CatalogService.DAL.Models;
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

        public bool Delete(int id)
        {
            var deleted = data.DeleteProduct(id);
            data.Commit();
            return deleted;
        }

        public Product Get(int id)
        {
            return data.GetProductById(id);
        }

        public IEnumerable<Product> List()
        {
            return data.GetAllProducts();
        }

        public Product Update(Product product)
        {
            var ProductUpdated = data.UpdateProduct(product);
            data.Commit();
            return ProductUpdated;
        }
    }
}
