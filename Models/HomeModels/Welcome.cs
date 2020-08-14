using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMagazine.Models.HomeModels
{
    public class Welcome
    {
        public int Id { get; set; }
        public string ImageLink { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string GoalTitle { get; set; }
        public IList<Purpose> Purposes { get; set; }
    }
}
