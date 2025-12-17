using Microsoft.AspNetCore.Mvc;
using server.Bll.Interfaces;
using server.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchasesService purchasesService;

        public PurchasesController(IPurchasesService service)
        {
            purchasesService = service;
        }

        [HttpGet("tickets-by-gift/{giftId}")]
        public IActionResult GetTicketsByGift(int giftId)
        {
            try
            {
                var result = purchasesService.GetTicketsByGiftId(giftId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("gifts-by-price")]
        public IActionResult GetGiftsByPrice()
        {
            var result = purchasesService.GetGiftsSortedByPrice();
            return Ok(result);
        }

        [HttpGet("gifts-by-buyers")]
        public IActionResult GetGiftsByBuyersNumber()
        {
            var result = purchasesService.GetGiftsSortedByPurchases();
            return Ok(result);
        }

        [HttpGet("buyers-by-gift/{giftId}")]
        public IActionResult GetBuyersByGift(int giftId)
        {
            try
            {
                var result = purchasesService.GetBuyersByGiftId(giftId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
