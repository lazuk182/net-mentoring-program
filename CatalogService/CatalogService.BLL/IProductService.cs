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
    }
}
