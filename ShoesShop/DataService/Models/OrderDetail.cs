﻿using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public int? ShoesId { get; set; }
        public int? Quantity { get; set; }
        public int? OrderId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Shoes Shoes { get; set; }
    }
}
