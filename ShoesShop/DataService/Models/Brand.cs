using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class Brand
    {
        public Brand()
        {
            Product = new HashSet<Product>();
            Shoes = new HashSet<Shoes>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Product { get; set; }
        public virtual ICollection<Shoes> Shoes { get; set; }
    }
}
