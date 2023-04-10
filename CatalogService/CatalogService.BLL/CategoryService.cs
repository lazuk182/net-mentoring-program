using CatalogService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.BLL
{
    public class CategoryService : ICategoryService
    {
        private readonly DAL.ICatalogData data;
        public CategoryService(DAL.ICatalogData data) 
        {
            this.data = data;
        }

        public Category Add(Category category)
        {
            var CategoryAdded = data.AddCategory(category);
            data.Commit();
            return CategoryAdded;
        }

        public async Task<Category> AddAsync(Category category)
        {
            var CategoryAdded = data.AddCategory(category);
            await data.CommitAsync();
            return CategoryAdded;
        }

        public bool Delete(int id)
        {
            bool deleted = data.DeleteCategory(id);
            data.Commit();
            return deleted;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool deleted = data.DeleteCategory(id);
            await data.CommitAsync();
            return deleted;
        }

        public Category Get(int id)
        {
            return data.GetCategoryById(id);
        }

        public Task<Category> GetAsync(int id)
        {
            return data.GetCategoryByIdAsync(id);
        }

        public IEnumerable<Category> List()
        {
            return data.GetAllCategories();
        }

        public async Task<List<Category>> ListAsync()
        {
            return await data.GetAllCategoriesAsync();
        }

        public Category Update(Category category)
        {
            Category categoryUpdated = data.UpdateCategory(category);
            data.Commit();
            return categoryUpdated;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            Category categoryUpdated = data.UpdateCategory(category);
            await data.CommitAsync();
            return categoryUpdated;
        }
    }
}
