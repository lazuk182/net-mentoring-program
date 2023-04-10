using CatalogService.BLL;
using CatalogService.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductService _itemRepository;
        private readonly ICategoryService _categoryRepository;

        public CatalogController(IProductService itemRepository, ICategoryService categoryRepository)
        {
            _itemRepository = itemRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _categoryRepository.ListAsync();
            return Ok(categories);
        }

        [HttpGet("items")]
        public async Task<ActionResult<IEnumerable<Product>>> GetItems([FromQuery] int? categoryId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var items = await _itemRepository.ListAsync(categoryId, pageNumber, pageSize);
            if (items != null)
                return Ok(items);
            else
                return NotFound();
        }

        [HttpPost("categories")]
        public async Task<ActionResult<Category>> AddCategory([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addedCategory = await _categoryRepository.AddAsync(category);
            return CreatedAtAction(nameof(GetCategories), new { id = addedCategory.Id }, addedCategory);
        }

        [HttpPost("items")]
        public async Task<ActionResult<Product>> AddItem([FromBody] Product item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addedItem = await _itemRepository.AddAsync(item);
            return CreatedAtAction(nameof(GetItems), new { id = addedItem.Id }, addedItem);
        }

        [HttpPut("categories/{id}")]
        public async Task<ActionResult<Category>> UpdateCategory(int id, [FromBody] Category category)
        {
            if (id != category.Id)
            {
                return BadRequest("Id mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedCategory = await _categoryRepository.UpdateAsync(category);
            return Ok(updatedCategory);
        }

        [HttpPut("items/{id}")]
        public async Task<ActionResult<Product>> UpdateItem(int id, [FromBody] Product item)
        {
            if (id != item.Id)
            {
                return BadRequest("Id mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedItem = await _itemRepository.UpdateAsync(item);
            return Ok(updatedItem);
        }

        [HttpDelete("items/{id}")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            await _itemRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpDelete("categories/{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            await _categoryRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
