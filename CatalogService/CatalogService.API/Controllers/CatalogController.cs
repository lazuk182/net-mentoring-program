using CatalogService.API.DTO;
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
        private readonly IRabbitMQProducer _rabbitMQ;
        public CatalogController(IProductService itemRepository, ICategoryService categoryRepository, IRabbitMQProducer rabbitMQ)
        {
            _itemRepository = itemRepository;
            _categoryRepository = categoryRepository;
            _rabbitMQ = rabbitMQ;
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
        public async Task<ActionResult<Category>> AddCategory(DTO.AddCategoryRequest category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Category categoryToStore = new Category
            {
                Image = category.Image,
                Name = category.Name
            };
            var addedCategory = await _categoryRepository.AddAsync(categoryToStore);
            return CreatedAtAction(nameof(GetCategories), new { id = addedCategory.Id }, addedCategory);
        }

        [HttpPost("items")]
        public async Task<ActionResult<Product>> AddItem([FromBody] AddProductRequest item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Product productToStore = new Product
            {
                Amount = item.Amount,
                Name = item.Name,
                CategoryId = item.CategoryId,
                Image = item.Image,
                Description = item.Description,
                Price = item.Price
            };
            var addedItem = await _itemRepository.AddAsync(productToStore);
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
        public async Task<ActionResult<Product>> UpdateItem(int id, [FromBody] AddProductRequest item)
        {
            Product productToUpdate = await _itemRepository.GetAsync(id);
            if (productToUpdate == null)
                return NotFound();

            if (id != productToUpdate.Id)
            {
                return BadRequest("Id mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            productToUpdate.Amount = item.Amount;
            productToUpdate.Description = item.Description;
            productToUpdate.Price = item.Price;
            productToUpdate.Name = item.Name;
            productToUpdate.CategoryId = item.CategoryId;
            productToUpdate.Image = item.Image;
            var updatedItem = await _itemRepository.UpdateAsync(productToUpdate);
            var ProductCart = new DTO.ProductCart
            {
                Id = productToUpdate.Id,
                Image = productToUpdate.Image,
                Name = productToUpdate.Name,
                Price = productToUpdate.Price
            };
            _rabbitMQ.SendProductMessage(ProductCart);
            /*
             POST =>
                {
                  "id": 1,
                  "name": "prueba modificada",
                  "image": "datos de imagen",
                  "price": 10
                }
                https://localhost:7068/api/v2/Item
             * */
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
