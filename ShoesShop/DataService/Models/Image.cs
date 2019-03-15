using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class Image
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public int? OwnId { get; set; }
        public bool? IsShoes { get; set; }
    }
}
