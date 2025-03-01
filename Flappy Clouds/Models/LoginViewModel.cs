using System.ComponentModel.DataAnnotations;

namespace Flappy_Clouds.Models
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Email is required")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Max 20 or minimum 5 characters is allowed  ")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
