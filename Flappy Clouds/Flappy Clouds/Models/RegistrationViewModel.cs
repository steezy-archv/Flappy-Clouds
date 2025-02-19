using System.ComponentModel.DataAnnotations;

namespace Flappy_Clouds.Models
{
    public class RegistrationViewModel
    {

        [Required(ErrorMessage = "Username is required")]
        [MaxLength(20, ErrorMessage = "Max 20 characters is allowed  ")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Firstname is required")]
        [MaxLength(50, ErrorMessage = "Max 50 characters is allowed  ")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lastname is required")]
        [MaxLength(50, ErrorMessage = "Max 50 characters is allowed  ")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [MaxLength(100, ErrorMessage = "Max 100 characters is allowed  ")]
        //[EmailAddress(ErrorMessage = "Please enter a valid Email Address")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please Enter Valid Email.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength =5 ,ErrorMessage = "Max 20 or minimum 5 characters is allowed  ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password",ErrorMessage = "Please Confirm your password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}
