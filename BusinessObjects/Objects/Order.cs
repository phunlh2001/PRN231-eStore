using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Objects
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int MemberId { get; set; }
        public string OrderDate { init; get; } = DateTime.Now.ToString("dd/MM/yyyy");
        public string RequiredDate { get; set; }
        public string ShippedDate { get; set; }
        [Required] public decimal Freight { get; set; }

        public Member Member { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
