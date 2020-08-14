using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMagazine.Models.HomeModels
{
    public class Home
    {
        public int Id { get; set; }
        public ICollection<CarouselItem> CarouselItems { get; set; }
        public Welcome Welcome { get; set; }
        public ICollection<Topic> Topics { get; set; }
    }
}
