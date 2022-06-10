using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieMVC.Models;

namespace MovieMVC.Data
{
    public class MovieContext:DbContext
    {
        public MovieContext (DbContextOptions<MovieContext> options): base(options)
        {}

        public DbSet<Category> Categories { get; set; }
        public DbSet<Movie> Movies { get; set; }

    }
}
