using System;
using System.Collections.Generic;

namespace ProjectTraining.Models
{
    public partial class Comments
    {
        public int? ProductId { get; set; }
        public int? UserId { get; set; }
        public string Comment { get; set; }
        public int? Rate { get; set; }
        public int CommentId { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
