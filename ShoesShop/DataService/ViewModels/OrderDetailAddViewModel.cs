using DataService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.ViewModels
{
    public class OrderDetailAddViewModel
    {
        public int? ShoesId { get; set; }
        public int? Quantity { get; set; }
        public int? SizeId { get; set; }
    }
}
