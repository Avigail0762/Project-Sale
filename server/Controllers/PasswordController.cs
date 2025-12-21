using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using BCrypt.Net;



namespace server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        // GET: api/<PasswordController>
        [HttpGet]
        public IEnumerable<string> Get()
        {

            return new string[] { "value1", "value2" };
        }

        // GET api/<PasswordController>/5
        [HttpGet("{pass}")]
        public string Get(string pass)
        {
            return HashPassword(pass);
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
