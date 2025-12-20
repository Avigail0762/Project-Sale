using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Bll.Interfaces;
using server.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "manager")]
    public class LotteryController : ControllerBase
    {
        private readonly ILotteryService _lotteryService;

        public LotteryController(ILotteryService lotteryService)
        {
            _lotteryService = lotteryService;
        }

        // POST: api/lottery/draw/{giftId}
        // מבצע הגרלה על מתנה ספציפית ומחזיר את הכרטיס המנצח
        [HttpPost("draw/{giftId}")]
        public ActionResult<Ticket> Draw(int giftId)
        {
            try
            {
                var winnerTicket = _lotteryService.DoLottery(giftId);
                return Ok(winnerTicket);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/lottery/winners
        // מחזיר את כל הזוכים לכל המתנות
        [HttpGet("winners")]
        public ActionResult<List<Ticket>> GetWinners()
        {
            var winners = _lotteryService.GetWinnersReport();
            if (winners == null || winners.Count == 0)
                return NoContent();

            return Ok(winners);
        }

        // GET: api/lottery/total-income
        // מחזיר את סך ההכנסות מכל הרכישות
        [HttpGet("total-income")]
        public ActionResult<decimal> GetTotalIncome()
        {
            return Ok(_lotteryService.GetTotalIncome());
        }

    }

}
