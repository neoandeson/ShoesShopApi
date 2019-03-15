using DataService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.ViewModels
{
    public class OrderAddViewModel
    {
        public DateTime? CreateDate { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        public string ShipAddress { get; set; }
        public DateTime? ShipDate { get; set; }
        public string ContactPhone { get; set; }
        public string CusName { get; set; }
        public string DiscountCode { get; set; }

        public List<OrderDetailAddViewModel> OrderDetailAdds { get; set; }
    }
}
