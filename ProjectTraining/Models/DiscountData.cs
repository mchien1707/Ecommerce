using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTraining.Models
{
    public class DiscountData
    {
        public DiscountData()
        {
            Product = new HashSet<Product>();
        }

        public int DiscountId { get; set; }
        public double? DiscountPercent { get; set; }
        public string DiscountStart { get; set; }
        public string DiscountEnd { get; set; }
        public string DiscountDescription { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
