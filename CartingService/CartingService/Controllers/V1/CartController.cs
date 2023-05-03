using CartingService.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;

namespace CartingService.API.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // GET: api/<CartController>
        [HttpGet]
        [Authorize(Roles = "manager, buyer")]
        public IActionResult GetCart(int cartKey)
        {
            var cart = _cartService.GetCartById(cartKey);
            if (cart == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(cart);
            }
        }

        // Add item to cart
        [HttpPost]
        [Authorize(Roles = "manager, buyer")]
        public ActionResult AddItemToCart(int cartKey, DAL.Models.Item item)
        {
            try
            {
                var cart = _cartService.GetCartById(cartKey);
                _cartService.AddItemToCart(item, cart.Id);
                cart = _cartService.GetCartById(cartKey);
                return Ok(cart);
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

        // Delete item from cart
        [HttpDelete]
        [Authorize(Roles = "manager, buyer")]
        public ActionResult DeleteItemFromCart(int cartKey, int itemId)
        {
            try
            {
                var cart = _cartService.GetCartById(cartKey);
                _cartService.RemoveItemFromCart(itemId, cart.Id);
                cart = _cartService.GetCartById(cartKey);
                return Ok(cart);
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
