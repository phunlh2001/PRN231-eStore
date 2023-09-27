using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Objects
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }
        [Required(ErrorMessage = "Email cannot be empty"), StringLength(100)]
        [EmailAddress(ErrorMessage = "The email invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Company cannot be empty"), StringLength(40)]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "City cannot be empty"), StringLength(15)]
        public string City { get; set; }

        [Required(ErrorMessage = "Country cannot be empty"), StringLength(15)]
        public string Country { get; set; }

        [Required(ErrorMessage = "Password cannot be empty"), StringLength(30), DataType(DataType.Password)]
        public string Password { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
