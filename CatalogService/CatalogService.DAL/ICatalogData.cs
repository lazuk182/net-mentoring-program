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

        #region Products
        Models.Product GetProductById(int id);
        IEnumerable<Models.Product> GetAllProducts();
        Models.Product AddProduct(Models.Product product);
        bool DeleteProduct(int id);
        Models.Product UpdateProduct(Models.Product product);
        #endregion

        #region Categories
        Models.Category GetCategoryById(int id);
        IEnumerable<Models.Category> GetAllCategories();
        Models.Category AddCategory(Models.Category Category);
        bool DeleteCategory(int id);
        Models.Category UpdateCategory(Models.Category Category);
        #endregion
    }
}
