using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Objects
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
