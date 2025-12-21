using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Bll.Interfaces;
using server.Models;
using server.Models.DTO;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "manager")]
    public class GiftController : ControllerBase
    {
        private readonly IGiftService giftService;

        public GiftController(IGiftService giftService)
        {
            this.giftService = giftService;
        }

        // GET: api/gift
        [HttpGet]
        public ActionResult<List<Gift>> Get()
        {
            var gifts = giftService.Get();

            if (gifts == null || gifts.Count == 0)
                return NoContent();

            return Ok(gifts);
        }

        // GET: api/gift/{id}
        [HttpGet("{id}")]
        public ActionResult<Gift> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid gift id");

            var gift = giftService.GetById(id);
            if (gift == null)
                return NotFound($"Gift with id {id} not found");

            return Ok(gift);
        }

        // GET: api/gift/name/{name}
        [HttpGet("name/{name}")]
        public ActionResult<Gift> GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Gift name is required");

            var gift = giftService.GetByName(name);
            if (gift == null)
                return NotFound("Gift not found");

            return Ok(gift);
        }

        // GET: api/gift/category/{category}
        [HttpGet("category/{category}")]
        public ActionResult<List<Gift>> GetByCategory(string category)
        {
            if (string.IsNullOrEmpty(category))
                return BadRequest("Category is required");

            var gifts = giftService.GetByCategory(category);
            if (gifts == null || gifts.Count == 0)
                return NoContent();

            return Ok(gifts);
        }

        // GET: api/gift/donor?firstName=...&lastName=...
        [HttpGet("donor")]
        public ActionResult<List<Gift>> GetByDonorName(
            [FromQuery] string firstName,
            [FromQuery] string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                return BadRequest("First name and last name are required");

            var gifts = giftService.GetByDonorName(firstName, lastName);
            if (gifts == null || gifts.Count == 0)
                return NoContent();

            return Ok(gifts);
        }

        // GET: api/gift/buyers/{number}
        [HttpGet("buyers/{number}")]
        public ActionResult<List<Gift>> GetByBuyersNumber(int number)
        {
            if (number < 0)
                return BadRequest("Invalid buyers number");

            var gifts = giftService.GetByBuyersNumber(number);
            if (gifts == null || gifts.Count == 0)
                return NoContent();

            return Ok(gifts);
        }

        // POST: api/gift
        [HttpPost]
        public ActionResult<Gift> Add([FromBody] GiftDTO gift)
        {
            Console.WriteLine("Im controller");
            if (gift == null)
                return BadRequest("Gift data is required");

            try
            {
                var newGift = giftService.Add(gift);
                return CreatedAtAction(
                    nameof(GetById),
                    new { id = newGift.Id },
                    newGift
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/gift/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] GiftDTO gift)
        {
            if (id <= 0)
                return BadRequest("Invalid gift id");

            if (gift == null)
                return BadRequest("Gift data is required");

            try
            {
                giftService.Update(id, gift);
                return Ok();
            }
            catch
            {
                return NotFound($"Gift with id {id} not found");
            }
        }

        // DELETE: api/gift/{id}
        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid gift id");

            var result = giftService.Remove(id);
            if (!result)
                return NotFound($"Gift with id {id} not found");

            return Ok();
        }
        // GET: api/gift/sorted?ascending=true
        [HttpGet("sorted")]
        public ActionResult<List<Gift>> GetByPrice([FromQuery] bool ascending = true)
        {
            var gifts = giftService.GetByPrice(ascending);
            if (gifts == null || gifts.Count == 0)
                return NoContent();

            return Ok(gifts);
        }

    }
}