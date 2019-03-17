using DataService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.ViewModels
{
    public class ShoesHasSizeViewModel
    {
        public int Id { get; set; }
        public int ShoesId { get; set; }
        public string ShoesName { get; set; }
        public int SizeId { get; set; }
        public int Quantity { get; set; }
        public int Scale { get; set; }
    }
}
