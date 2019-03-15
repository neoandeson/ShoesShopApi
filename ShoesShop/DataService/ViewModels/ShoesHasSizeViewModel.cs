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
        public int SizeId { get; set; }
        public int Quantity { get; set; }

        public virtual Size Size { get; set; }
    }
}
