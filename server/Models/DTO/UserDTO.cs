using System.ComponentModel.DataAnnotations;

namespace server.Models.DTO
{
    public class UserDTO
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Username { get; set; }
        [Phone]
        [MaxLength(20)]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
    }
}
