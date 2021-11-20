using System;
using System.Collections.Generic;

namespace ProjectTraining.Models
{
    public partial class Discount
    {
        public Discount()
        {
            Product = new HashSet<Product>();
        }

        public int DiscountId { get; set; }
        public double? DiscountPercent { get; set; }
        public DateTime? DiscountStart { get; set; }
        public DateTime? DiscountEnd { get; set; }
        public string DiscountDescription { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
