using System;
using System.Collections.Generic;

namespace ProjectTraining.Models
{
    public partial class Product
    {
        public Product()
        {
            Comments = new HashSet<Comments>();
            OrderDetails = new HashSet<OrderDetails>();
            Warehouse = new HashSet<Warehouse>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double? ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public int? DiscountId { get; set; }
        public int? KindId { get; set; }
        public string ProductDescription { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? Quantities { get; set; }

        public virtual Discount Discount { get; set; }
        public virtual Kind Kind { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual ICollection<Warehouse> Warehouse { get; set; }
    }
}
