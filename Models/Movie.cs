﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMVC.Models
{
    public class Movie
    {
        public int MovieID { get; set; }
        public string Title { get; set; }
        public string Cover { get; set; }
        public string Year { get; set; }
        public string Genres { get; set; }
        public string Description { get; set; }
    }
}
