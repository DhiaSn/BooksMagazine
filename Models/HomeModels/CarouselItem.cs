using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMagazine.Models.HomeModels
{
    public class CarouselItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Quote { get; set; }
        public string ImageLink { get; set; }
    }
}
