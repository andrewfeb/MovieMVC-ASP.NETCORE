using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MovieMVC.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string GenreName { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
