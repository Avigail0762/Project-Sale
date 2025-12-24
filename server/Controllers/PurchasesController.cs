using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Bll.Interfaces;
using server.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "manager")]

    public class PurchasesController : ControllerBase
    {
        private readonly IPurchasesService purchasesService;
        private readonly ILogger<PurchasesController> logger;


        public PurchasesController(IPurchasesService service, ILogger<PurchasesController> logger)
        {
            purchasesService = service;
            this.logger = logger;
        }

        [HttpGet("tickets-by-gift/{giftId}")]
        public IActionResult GetTicketsByGift(int giftId)
        {
            logger.LogInformation("GetTicketsByGift started. GiftId={GiftId}", giftId);
            try
            {
                var result = purchasesService.GetTicketsByGiftId(giftId);
                logger.LogInformation("GetTicketsByGift finished successfully. GiftId={GiftId}", giftId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in GetTicketsByGift. GiftId={GiftId}", giftId);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("gifts-by-price")]
        public IActionResult GetGiftsByPrice()
        {
            logger.LogInformation("GetGiftsByPrice started");
            var result = purchasesService.GetGiftsSortedByPrice();
            logger.LogInformation("GetGiftsByPrice finished successfully");

            return Ok(result);
        }

        [HttpGet("gifts-by-buyers")]
        public IActionResult GetGiftsByBuyersNumber()
        {
            logger.LogInformation("GetGiftsByBuyersNumber started");

            var result = purchasesService.GetGiftsSortedByPurchases();
            logger.LogInformation("GetGiftsByBuyersNumber finished successfully");

            return Ok(result);
        }

        [HttpGet("buyers-by-gift/{giftId}")]
        public IActionResult GetBuyersByGift(int giftId)
        {
            logger.LogInformation("GetBuyersByGift started. GiftId={GiftId}", giftId);

            try
            {
                var result = purchasesService.GetBuyersByGiftId(giftId);
                logger.LogInformation("GetBuyersByGift finished successfully. GiftId={GiftId}", giftId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in GetBuyersByGift. GiftId={GiftId}", giftId);
                return BadRequest(ex.Message);
            }
        }
    }

}
