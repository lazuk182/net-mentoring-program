using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.BLL
{
    public interface ICategoryService
    {
        public DAL.Models.Category Get(int id);
        public IEnumerable<DAL.Models.Category> List();
        public DAL.Models.Category Add(DAL.Models.Category category);
        public DAL.Models.Category Update(DAL.Models.Category category);
        public bool Delete(int id);
    }
}
