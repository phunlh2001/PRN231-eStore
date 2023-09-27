using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.DTOs
{
    public class LoginInfo
    {
        [Required(ErrorMessage = "Email cannot be empty")]
        [EmailAddress(ErrorMessage = "The email is invalid"), StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password cannot be empty"), DataType(DataType.Password), MinLength(5)]
        public string Password { get; set; }
    }
}
