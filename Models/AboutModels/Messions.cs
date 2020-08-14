using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMagazine.Models.AboutModels
{
    public class Messions
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageLink { get; set; }
        public ICollection<Purpose> Purposes { get; set; }
    }
}