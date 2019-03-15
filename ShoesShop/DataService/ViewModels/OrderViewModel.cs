using DataService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        public string ShipAddress { get; set; }
        public string ShipDate { get; set; }
        public string ContactPhone { get; set; }
        public string CusName { get; set; }
        public int? DiscountCode { get; set; }
        public double? Discount { get; set; }
        public double? Total { get; set; }
    }
}
