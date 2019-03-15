using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public double? Price { get; set; }
        public int? BrandId { get; set; }
        public bool? IsAvailable { get; set; }
        public string Description { get; set; }
        public int? Sex { get; set; }

        public virtual Brand Brand { get; set; }
    }
}
