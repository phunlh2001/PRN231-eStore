using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Objects
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Category cannot be empty")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Product Name cannot be empty")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Weight cannot be empty")]
        public string Weight { get; set; }

        [Required(ErrorMessage = "Price cannot be empty"), Range(1, 1_000_000, ErrorMessage = "Price must be greater than 0")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Stock cannot be empty"), Range(1, 500, ErrorMessage = "Stock must be 1-500")]
        public int UnitslnStock { get; set; }

        public Category Category { get; set; }
    }
}
