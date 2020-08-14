using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMagazine.Models.AboutModels
{
    public class Team
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IList<Worker> Workers { get; set; }
    }
}
