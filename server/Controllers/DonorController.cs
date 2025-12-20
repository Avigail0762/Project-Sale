using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Bll.Interfaces;
using server.Models;
using server.Models.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "manager")]
    public class DonorController : ControllerBase
    {
        private readonly IDonorService donorService;
        public DonorController(IDonorService donorService)
        {
            this.donorService = donorService;
        }

        // GET: api/donor
        [HttpGet]
        public ActionResult<List<Donor>> Get()
        {
            var donors = donorService.Get();

            if (donors == null || donors.Count == 0)
                return NoContent();

            return Ok(donors);
        }

        // GET: api/donor/email/{email}
        [HttpGet("email/{email}")]
        public ActionResult<Donor> GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest("Email is required");

            var donor = donorService.GetByEmail(email);
            if (donor == null)
                return NotFound($"Donor with email '{email}' not found");

            return Ok(donor);
        }

        // GET: api/donor/name?firstName=...&lastName=...
        [HttpGet("name")]
        public ActionResult<Donor> GetByName(
            [FromQuery] string firstName,
            [FromQuery] string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                return BadRequest("First name and last name are required");

            var donor = donorService.GetByName(firstName, lastName);
            if (donor == null)
                return NotFound("Donor not found");

            return Ok(donor);
        }

        // POST: api/donor
        [HttpPost]
        public ActionResult<Donor> Add([FromBody] DonorDTO donor)
        {
            if (donor == null)
                return BadRequest("Donor data is required");

            var newDonor = donorService.Add(donor);

            return CreatedAtAction(
                nameof(GetByEmail),
                new { email = newDonor.Email },
                newDonor
            );
        }

        // PUT: api/donor/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] DonorDTO donor)
        {
            if (id <= 0)
                return BadRequest("Invalid donor id");

            if (donor == null)
                return BadRequest("Donor data is required");

            try
            {
                donorService.Update(id, donor);
                return Ok();
            }
            catch
            {
                return NotFound($"Donor with id {id} not found");
            }
        }

        // DELETE: api/donor/{id}
        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid donor id");

            var result = donorService.Remove(id);
            if (!result)
                return NotFound($"Donor with id {id} not found");

            return Ok();
        }
    }

}
