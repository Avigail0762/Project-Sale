using Microsoft.AspNetCore.Mvc;
using server.Bll.Interfaces;
using server.Models.DTO;

namespace server.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // ---------- AUTH ----------

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDTO dto)
        {
            var user = _customerService.Register(dto);
            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login([FromQuery] string email)
        {
            var user = _customerService.Login(email);
            return Ok(user);
        }

        // ---------- GIFTS ----------

        [HttpGet("gifts")]
        public IActionResult GetGifts(
            [FromQuery] string? category,
            [FromQuery] bool? sortPriceAsc)
        {
            var gifts = _customerService.GetGifts(category, sortPriceAsc);
            return Ok(gifts);
        }

        // ---------- CART ----------

        [HttpPost("cart/add")]
        public IActionResult AddToCart(
            [FromQuery] int userId,
            [FromQuery] int giftId)
        {
            _customerService.AddToCart(userId, giftId);
            return Ok();
        }

        [HttpDelete("cart/remove")]
        public IActionResult RemoveFromCart(
            [FromQuery] int userId,
            [FromQuery] int giftId)
        {
            _customerService.RemoveFromCart(userId, giftId);
            return Ok();
        }

        // ---------- PURCHASE ----------

        [HttpPost("purchase")]
        public IActionResult Purchase([FromQuery] int userId)
        {
            _customerService.Purchase(userId);
            return Ok();
        }
    }
}
