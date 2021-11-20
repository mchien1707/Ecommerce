using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTraining.Models
{
    public class ItemCart
    {
        public int ProductId { get; set; }
        public int? Quantities { get; set; }
        public virtual Product Product { set; get; }
        //public int? Price { get; set; }
    }
}
