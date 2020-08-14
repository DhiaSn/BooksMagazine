using BooksMagazine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMagazine.ViewModels.ContactUsViewModels
{
    public class CreateContactUsViewModel : ContactUs
    {
        // This Variable for detacting OverPosting Atack
        public string Topic { get; set; }
    }
}
