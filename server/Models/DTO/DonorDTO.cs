using System.ComponentModel.DataAnnotations;

namespace server.Models.DTO
{
    public class DonorDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
