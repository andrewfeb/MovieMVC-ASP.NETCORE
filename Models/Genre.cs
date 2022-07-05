using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMVC.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
