using System;
using System.Collections.Generic;

namespace ProjectTraining.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public int OrderId { get; set; }
        public double? Total { get; set; }
        public int? Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UserId { get; set; }
        public int? Type { get; set; }
        public int? PaymentResult { get; set; }
        public string Note { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
