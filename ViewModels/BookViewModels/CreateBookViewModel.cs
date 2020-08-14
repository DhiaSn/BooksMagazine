using BooksMagazine.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMagazine.ViewModels.BookViewModels
{
    public class CreateBookViewModel : Book 
    {
        [Display(Name = "Cover Image")]
        [Required]
        public Microsoft.AspNetCore.Http.IFormFile CoverImageFile { get; set; }
        [Display(Name = "Book")]
        [Required]
        public Microsoft.AspNetCore.Http.IFormFile BookFile { get; set; }
        // This Variable for detacting OverPosting Atack
        public string History { get; set; }
    }
}
