using DataService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.ViewModels
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }
        public Shoes Shoes { get; set; }
        public int? Quantity { get; set; }
        public Size Size { get; set; }
    }
}
