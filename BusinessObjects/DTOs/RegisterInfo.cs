using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.DTOs
{
    public class RegisterInfo
    {
        [Required(ErrorMessage = "Email cannot be empty"), StringLength(100)]
        [EmailAddress(ErrorMessage = "The email invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Company cannot be empty"), StringLength(40)]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "City cannot be empty"), StringLength(15)]
        public string City { get; set; }

        [Required(ErrorMessage = "Country cannot be empty"), StringLength(15)]
        public string Country { get; set; }

        [Required(ErrorMessage = "Password cannot be empty"), DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "Password min length is 5")]
        [MaxLength(20, ErrorMessage = "Maximum is 20")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password cannot be empty"), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
