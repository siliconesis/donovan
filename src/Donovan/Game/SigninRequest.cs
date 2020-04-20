using System.ComponentModel.DataAnnotations;

namespace Donovan.Game
{
    public class SigninRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
