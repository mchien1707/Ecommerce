using System;
using System.Collections.Generic;

namespace ProjectTraining.Models
{
    public partial class Warehouse
    {
        public int ProductId { get; set; }
        public int? Quantities { get; set; }
        public double? ImportPrice { get; set; }
        public double? ExportPrice { get; set; }
        public DateTime? DateImport { get; set; }
        public int? Solded { get; set; }
        public int WarehouseId { get; set; }

        public virtual Product Product { get; set; }
    }
}
