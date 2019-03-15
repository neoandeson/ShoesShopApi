using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class ShoesHasSize
    {
        public int Id { get; set; }
        public int ShoesId { get; set; }
        public int SizeId { get; set; }
        public int Quantity { get; set; }

        public virtual Shoes Shoes { get; set; }
        public virtual Size Size { get; set; }
    }
}
