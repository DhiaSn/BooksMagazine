using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMagazine.Models.AboutModels
{
    public class About
    {
        public int Id { get; set; }
        public AboutUs AboutUs { get; set; }
        public Messions Messions { get; set; }
        public Messions Vision { get; set; }
        public Team Team { get; set; }
    }
}