using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.DTOs
{
    public class OrderInfo
    {
        public decimal Freight { get; set; }
        [Required] public int Quantity { get; set; }
    }
}
