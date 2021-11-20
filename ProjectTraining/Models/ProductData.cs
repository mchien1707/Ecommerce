using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTraining.Models
{
    public class ProductData
    {
        public ProductData()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double? ImportPrice { get; set; }
        public int? Quantities { get; set; }
        public double? ProductPrice { get; set; }
        public IFormFile ProductImage { get; set; }
        public string ProductImageName { get; set; }
        public int? DiscountId { get; set; }
        public int? KindId { get; set; }
        public string ProductDescription { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual Discount Discount { get; set; }
        public virtual Kind Kind { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
