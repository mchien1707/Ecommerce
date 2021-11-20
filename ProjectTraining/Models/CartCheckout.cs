using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTraining.Models
{
    public class CartCheckout
    {
        public virtual List<ItemCart> ListItemCart { set; get; }
        public User User { set; get; }
    }
}
