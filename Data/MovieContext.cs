using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieMVC.Models;
using MovieMVC.Data.EntityConfiguration;

namespace MovieMVC.Data
{
    public class MovieContext:DbContext
    {
        public MovieContext (DbContextOptions<MovieContext> options): base(options)
        {}

        public DbSet<Category> Categories { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MovieEntityConfig());
            modelBuilder.ApplyConfiguration(new CategoryEntityConfig());
            modelBuilder.ApplyConfiguration(new GenreEntityConfig());            
            
            //Seeding data
            modelBuilder.Entity<Category>()
                .HasData(new { Id = 1, CategoryName = "TV Drama" });
        }
    }
}
