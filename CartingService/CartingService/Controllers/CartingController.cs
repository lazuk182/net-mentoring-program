using CartingService.BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartingController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartingController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpPost("CreateCart")]
        public IActionResult CreateCart()
        {
            _cartService.Initialize();            
            return Ok(_cartService.GetCartId());
        }

        [HttpGet("GetListOfItems")]
        public IActionResult GetListOfItems(int cartId)
        {
            _cartService.Initialize(cartId);
            return Ok(_cartService.GetListOfItems());
        }

        [HttpPost("AddItemToCart")]
        public IActionResult AddItemToCart(int cartId, DTOs.AddItemRequest Item)
        {
            _cartService.Initialize(cartId);
            BLL.Entities.Item newItem = new BLL.Entities.Item()
            {
                Name = Item.Name,
                Price = Item.Price
            };

            return Ok(_cartService.AddItemToCart(newItem));
        }

        [HttpPost("RemoveItemFromCart")]
        public IActionResult RemoveItemFromCart(int cartId, int itemId)
        {
            _cartService.Initialize(cartId);
            return Ok(_cartService.RemoveItemFromCart(itemId));
        }
    }
}