using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class Size
    {
        public Size()
        {
            OrderDetail = new HashSet<OrderDetail>();
            ShoesHasSize = new HashSet<ShoesHasSize>();
        }

        public int Id { get; set; }
        public int Scale { get; set; }

        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
        public virtual ICollection<ShoesHasSize> ShoesHasSize { get; set; }
    }
}
