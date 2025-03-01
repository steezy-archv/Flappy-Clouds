using System.ComponentModel.DataAnnotations;

namespace Flappy_Clouds.Models
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Firstname is required")]
        [MaxLength(50, ErrorMessage = "Max 50 characters is allowed  ")]
        public required string FirstName { get; set; }


        [Required(ErrorMessage = "Lastname is required")]
        [MaxLength(50, ErrorMessage = "Max 50 characters is allowed  ")]
        public required string LastName { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [MaxLength(100, ErrorMessage = "Max 100 characters is allowed  ")]
        //[EmailAddress(ErrorMessage = "Please enter a valid Email Address")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please Enter Valid Email.")]
        public required string Email { get; set; }

        [StringLength(15)]
        public string? PhoneNumber { get; set; }


        [StringLength(255)]
        public string? Address { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength =5 ,ErrorMessage = "Max 20 or minimum 5 characters is allowed  ")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }


        [Compare("Password",ErrorMessage = "Please Confirm your password")]
        [DataType(DataType.Password)]
        public required string ConfirmPassword { get; set; }

    }
}
