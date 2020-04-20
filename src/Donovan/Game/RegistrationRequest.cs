using System.ComponentModel.DataAnnotations;

namespace Donovan.Game
{
    public class RegistrationRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
