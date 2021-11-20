﻿using System;
using System.Collections.Generic;

namespace ProjectTraining.Models
{
    public partial class OrderDetails
    {
        public int OrderDetailsId { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantities { get; set; }
        public double? TotalPrice { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
