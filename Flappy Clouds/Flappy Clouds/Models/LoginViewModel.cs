using System.ComponentModel.DataAnnotations;

namespace Flappy_Clouds.Models
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Username is required")]
        [MaxLength(20, ErrorMessage = "Max 20 characters is allowed  ")]
        public string UsernameorEmail { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Max 20 or minimum 5 characters is allowed  ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
