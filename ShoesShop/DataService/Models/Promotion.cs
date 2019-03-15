using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class Promotion
    {
        public Promotion()
        {
            Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string DiscountCode { get; set; }
        public int? Discount { get; set; }
        public string Description { get; set; }
        public string Event { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
