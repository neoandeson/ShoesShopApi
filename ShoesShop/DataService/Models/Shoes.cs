using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class Shoes
    {
        public Shoes()
        {
            OrderDetail = new HashSet<OrderDetail>();
            ShoesHasSize = new HashSet<ShoesHasSize>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public double? Price { get; set; }
        public bool? IsAvaiable { get; set; }
        public string Description { get; set; }
        public int Sex { get; set; }
        public int BrandId { get; set; }
        public string Avatar1 { get; set; }
        public string Avatar2 { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
        public virtual ICollection<ShoesHasSize> ShoesHasSize { get; set; }
    }
}
