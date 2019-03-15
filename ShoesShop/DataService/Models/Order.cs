using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        public string ShipAddress { get; set; }
        public DateTime? ShipDate { get; set; }
        public string ContactPhone { get; set; }
        public string CusName { get; set; }
        public string DiscountCode { get; set; }
        public double? Discount { get; set; }
        public double? Total { get; set; }
        public double? Sum { get; set; }

        public virtual Promotion DiscountCodeNavigation { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
