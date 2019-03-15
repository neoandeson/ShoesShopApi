using DataService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.ViewModels
{
    public class ShoesViewModel
    {
        public ShoesViewModel()
        {
            ShoesHasSizes = new List<ShoesHasSizeViewModel>();
            Images = new List<Image>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public double? Price { get; set; }
        public bool? IsAvaiable { get; set; }
        public string Description { get; set; }
        public int Sex { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public List<ShoesHasSizeViewModel> ShoesHasSizes { get; set; }
        public List<Image> Images { get; set; }
    }
}
