using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MovieMVC.Validation;

namespace MovieMVC.Models
{
    public class MovieViewModel
    {
        public int MovieId { get; set; }
        [Required]
        [StringLength(30)]
        public string Title { get; set; }    
        [Required]
        public int Category { get; set; }
        public string Year { get; set; }
        
        [DataType(DataType.Upload)]
        [MaxFileSize(1)] //file size MB
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile Cover { get; set; }
        public string CoverName { get; set; }
        public int[] Genres { get; set; }
        public string Description { get; set; }

    }
}
