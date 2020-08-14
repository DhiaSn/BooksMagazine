using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMagazine.Models.AboutModels
{
    public class Worker
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Profession { get; set; }
        public string ImageLink { get; set; }
        public string FacebookLink { get; set; }
        public string GoogleLink { get; set; }
        public string TwitterLink { get; set; }
        public string LinkedInLink { get; set; }
    }
}
