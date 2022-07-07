using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMVC.Models
{
    public class MovieViewModel
    {
        public int MovieId { get; set; }
        public string Title { get; set; }        
        public int Category { get; set; }
        public string Year { get; set; }
        public IFormFile Cover { get; set; }
        public string CoverName { get; set; }
        public int[] Genres { get; set; }
        public string Description { get; set; }

    }
}
