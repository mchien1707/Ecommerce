using System;
using System.Collections.Generic;

namespace ProjectTraining.Models
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comments>();
            Order = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }

        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
