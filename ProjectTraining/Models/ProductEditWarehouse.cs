using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTraining.Models
{
    public class ProductEditWarehouse
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public int? DiscountId { get; set; }
        public int? KindId { get; set; }
        public string ProductDescription { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? Quantities { get; set; }
        public double? ImportPrice { get; set; }
        public double? ExportPrice { get; set; }
        public DateTime? DateImport { get; set; }
        public int? Solded { get; set; }
        public int WarehouseId { get; set; }

        public int SLnhap { get; set; }

    }
}
