using DataService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.ViewModels
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }
        public int? ShoesId { get; set; }
        public int? Quantity { get; set; }
        public int? OrderId { get; set; }
    }
}
