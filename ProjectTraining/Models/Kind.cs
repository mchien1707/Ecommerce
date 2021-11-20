using System;
using System.Collections.Generic;

namespace ProjectTraining.Models
{
    public partial class Kind
    {
        public Kind()
        {
            Product = new HashSet<Product>();
        }

        public int KindId { get; set; }
        public string KindName { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
