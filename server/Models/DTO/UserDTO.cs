using System.ComponentModel.DataAnnotations;

namespace server.Models.DTO
{
    public class UserDTO
    {
        [Required]
        public string Name { get; set; }
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Roles { get; set; } = "user";
    }
}
