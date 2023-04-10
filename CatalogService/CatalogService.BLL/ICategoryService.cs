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
        public Task<DAL.Models.Category> GetAsync(int id);
        public IEnumerable<DAL.Models.Category> List();
        public Task<List<DAL.Models.Category>> ListAsync();
        public DAL.Models.Category Add(DAL.Models.Category category);
        public Task<DAL.Models.Category> AddAsync(DAL.Models.Category category);
        public DAL.Models.Category Update(DAL.Models.Category category);
        public Task<DAL.Models.Category> UpdateAsync(DAL.Models.Category category);
        public bool Delete(int id);
        public Task<bool> DeleteAsync(int id);
    }
}
