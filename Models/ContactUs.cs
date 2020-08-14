using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMagazine.Models
{
    public class ContactUs
    {
        public int Id { get; set; }
        [StringLength(20,MinimumLength =4,ErrorMessage ="Enter a real name...")]
        public string Name { get; set; }
        [RegularExpression("^[a-zA-Z-]+@[a-zA-Z-]+.[a-zA-Z]{2,6}$", ErrorMessage = "Enter a real Email number")]
        public string Email { get; set; }
        [RegularExpression("[0-9]{0,15}",ErrorMessage="Enter a real phone number")]
        [Display(Name="Phone Number")]
        public string PhoneNumber { get; set; }
        [RegularExpression("^[a-zA-Z1-3]*$", ErrorMessage = "Enter the Title without any special characters")]
        public string Subject { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Enter a real description")]
        [RegularExpression("^[a-zA-Z1-3., ?!]*$", ErrorMessage = "Enter the description without any special characters")]
        public string Description { get; set; }
    }
}
