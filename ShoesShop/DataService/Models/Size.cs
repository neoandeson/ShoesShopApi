using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class Size
    {
        public Size()
        {
            ShoesHasSize = new HashSet<ShoesHasSize>();
        }

        public int Id { get; set; }
        public int Size1 { get; set; }

        public virtual ICollection<ShoesHasSize> ShoesHasSize { get; set; }
    }
}
