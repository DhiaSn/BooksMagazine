using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMagazine.Models
{
    public class Book
    {
        public int Id { get; set; }
        //---
        [Required]
        [StringLength(20)]
        [RegularExpression("^[a-zA-Z1-3 ]*$", ErrorMessage = "Enter the Title without any special characters")]
        public string Type { get; set; }
        //---
        [Required]
        [StringLength(35,MinimumLength =3,ErrorMessage ="Enter a real title")]
        [RegularExpression("^[a-zA-Z1-3  ?!]*$", ErrorMessage = "Enter the Title without any special characters")]
        public string Title { get; set; }
        //---
        [StringLength(35, MinimumLength = 3, ErrorMessage = "Enter a real name")]
        [RegularExpression("^[a-zA-Z1-3 ]*$", ErrorMessage = "Enter the author without any special characters")]
        [Display(Name = "Author")]
        public string AuthorName { get; set; }
        //---
        [Display(Name = "Cover Image")]
        [Required]
        public string CoverImage { get; set; }
        //---
        [Required]
        [MinLength(5,ErrorMessage = "Enter a real description")]
        [RegularExpression("^[a-zA-Z1-3., ?!]*$", ErrorMessage = "Enter the description without any special characters")]
        public string Description { get; set; }
        //---
        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }
        //---
        [Display(Name ="Book")]
        public string Link { get; set; }
        //---
    }
}
