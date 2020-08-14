using BooksMagazine.Models.HomeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMagazine.ViewModels
{
    public class CreateCarouselItemViewModel : CarouselItem  
    {
        public Microsoft.AspNetCore.Http.IFormFile Image { get; set; }
        // This To Check Over Posting Atack
        public string Type { get; set; }

    }
}
