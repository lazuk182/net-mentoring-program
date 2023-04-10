using CatalogService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.BLL
{
    public interface IProductService
    {
        DAL.Models.Product Get(int id);
        IEnumerable<DAL.Models.Product> List();
        DAL.Models.Product Add(DAL.Models.Product product);
        DAL.Models.Product Update(DAL.Models.Product product);
        bool Delete(int id);

        Task<Product> GetAsync(int id);
        Task<List<Product>> ListAsync(int? categoryId, int pageNumber, int pageSize);
        Task<Product> AddAsync(DAL.Models.Product product);
        Task<DAL.Models.Product> UpdateAsync(DAL.Models.Product product);
        Task<bool> DeleteAsync(int id);
    }
}
