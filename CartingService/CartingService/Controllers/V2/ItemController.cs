using CartingService.BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CartingService.API.Controllers.V2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class ItemController : ControllerBase
    {
        private readonly ICartService _cartService;
        public ItemController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        public IActionResult UpdateItem(DTOs.UpdateItemInformation item)
        {
            try
            {
                DAL.Models.Item itemInDatabase = new DAL.Models.Item
                {
                    Id = item.Id,
                    Image = item.Image,
                    Name = item.Name,
                    Price = item.Price
                };
                var itemUpdated = _cartService.UpdateItemInformation(itemInDatabase);
                return Ok(itemUpdated);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException)
                {
                    return NotFound(ex.Message);
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
