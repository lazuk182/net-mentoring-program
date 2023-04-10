using CatalogService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.DAL
{
    public interface ICatalogData
    {
        int Commit();
        Task<int> CommitAsync();

        #region Products
        Models.Product GetProductById(int id);
        IEnumerable<Models.Product> GetAllProducts();
        Models.Product AddProduct(Models.Product product);
        bool DeleteProduct(int id);
        Models.Product UpdateProduct(Models.Product product);

        Task<Models.Product> GetProductByIdAsync(int id);
        Task<List<Models.Product>> GetAllProductsAsync();
        #endregion

        #region Categories
        Models.Category GetCategoryById(int id);
        IEnumerable<Models.Category> GetAllCategories();
        bool DeleteCategory(int id);
        Category AddCategory(Category category);
        Category UpdateCategory(Category category);

        Task<Models.Category> GetCategoryByIdAsync(int id);  
        Task<List<Models.Category>> GetAllCategoriesAsync();
        #endregion
    }
}
