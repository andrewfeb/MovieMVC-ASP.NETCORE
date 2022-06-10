using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieMVC.Models
{
    public class Movie
    {
        public int MovieID { get; set; }

        [Required]
        [Column(TypeName ="varchar(30)")]
        public string Title { get; set; }

        [Column(TypeName = "varchar(125)")]
        public string Cover { get; set; }

        [Column(TypeName = "char(4)")]
        public string Year { get; set; }

        [Column(TypeName = "varchar(125)")]
        public string Genres { get; set; }

        public string Description { get; set; }

        [Required]
        public Category Category { get; set; }
    }
}
