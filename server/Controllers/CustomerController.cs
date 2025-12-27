//using Micserver\Controllers\CustomerController.cs
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
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(
            ICustomerService customerService,
            ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        // ---------- AUTH ----------

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO dto)
        {
            _logger.LogInformation("Customer register started");

            if (dto == null)
            {
                _logger.LogWarning("Register failed - UserDTO is null");
                return BadRequest("User data is required");
            }

            try
            {
                var user = await _customerService.Register(dto);

                _logger.LogInformation("Customer registered successfully. Email={Email}", dto.Email);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during customer registration");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromQuery] string email)
        {
            _logger.LogInformation("Customer login started. Email={Email}", email);

            if (string.IsNullOrEmpty(email))
            {
                _logger.LogWarning("Login failed - email is empty");
                return BadRequest("Email is required");
            }

            try
            {
                var user = await _customerService.Login(email);

                _logger.LogInformation("Customer logged in successfully. Email={Email}", email);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during customer login. Email={Email}", email);
                return BadRequest(ex.Message);
            }
        }

        // ---------- GIFTS ----------

        [HttpGet("gifts")]
        public async Task<IActionResult> GetGifts(
            [FromQuery] string? category,
            [FromQuery] bool? sortPriceAsc)
        {
            _logger.LogInformation(
               "Get gifts started. Category={Category}, SortPriceAsc={SortPriceAsc}",
               category, sortPriceAsc);

            var gifts = await _customerService.GetGifts(category, sortPriceAsc);

            _logger.LogInformation("Get gifts finished successfully. Count={Count}", gifts.Count);
            return Ok(gifts);
        }

        // ---------- CART ----------

        [HttpPost("cart/add")]
        public async Task<IActionResult> AddToCart(
            [FromQuery] int userId,
            [FromQuery] int giftId)
        {
            _logger.LogInformation("Add to cart started. UserId={UserId}, GiftId={GiftId}", userId, giftId);

            await _customerService.AddToCart(userId, giftId);
            _logger.LogInformation("Gift added to cart successfully");
            return Ok();
        }

        [HttpDelete("cart/remove")]
        public async Task<IActionResult> RemoveFromCart(
            [FromQuery] int userId,
            [FromQuery] int giftId)
        {
            _logger.LogInformation("Remove from cart started. UserId={UserId}, GiftId={GiftId}", userId, giftId);

            await _customerService.RemoveFromCart(userId, giftId);
            _logger.LogInformation("Gift removed from cart successfully");
            return Ok();
        }

        // ---------- PURCHASE ----------

        [HttpPost("purchase")]
        public async Task<IActionResult> Purchase([FromQuery] int userId)
        {
            _logger.LogInformation("Purchase started. UserId={UserId}", userId);

            if (userId <= 0)
            {
                _logger.LogWarning("Invalid userId for purchase. UserId={UserId}", userId);
                return BadRequest("Invalid user id");
            }

            try
            {
                _logger.LogDebug("Calling Purchase service");

                await _customerService.Purchase(userId);

                _logger.LogInformation("Purchase completed successfully. UserId={UserId}", userId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during purchase. UserId={UserId}", userId);
                return BadRequest(ex.Message);
            }
        }
    }
}