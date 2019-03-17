using DataService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.ViewModels
{
    public class OrderInfoViewModel
    {
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

        public ICollection<OrderDetailViewModel> OrderDetail { get; set; }
    }
}
