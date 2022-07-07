using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieMVC.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string CategoryName { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
