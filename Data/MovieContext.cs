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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Custom configuration for category table
            modelBuilder.Entity<Category>()
                .Property(c => c.CategoryName)
                .HasColumnType("varchar(30)")
                .IsRequired();

            // Custom configuration for movie table
            modelBuilder.Entity<Movie>(
                em =>
                {
                    em.Property(m => m.Title).HasColumnType("varchar(30)").IsRequired();
                    em.Property(m => m.Cover).HasColumnType("varchar(125)");
                    em.Property(m => m.Year).HasColumnType("char(4)");
                    em.Property(m => m.Genres).HasColumnType("varchar(125)");
                });

            // One to many relationship with cascade delete
            modelBuilder.Entity<Movie>()
                .HasOne(m => m.Category)
                .WithMany(c => c.Movies)
                .HasForeignKey(m => m.CategoryID)
                .OnDelete(DeleteBehavior.Cascade);

            // One to one relationship with cascade delete
            /*modelBuilder.Entity<Movie>()
                .HasOne(m => m.Category)
                .WithOne(c => c.Movies)
                .HasForeignKey<Movie>(m => m.CategoryID)
                .OnDelete(DeleteBehavior.Cascade);*/

            // Many to Many relationship with cascade delete
            /*modelBuilder.Entity<Movie>()
                .HasMany(m => m.Categories)
                .WithMany(c => c.Movies);*/

            // Create Composite Key jika your create CategoryMovie Model
            /*modelBuilder.Entity<CategoryMovie>()
                .HasKey(t => new { t.CategoryID, t.MovieId });*/

            //Seedign data
            modelBuilder.Entity<Category>()
                .HasData(new { CategoryID = 1, CategoryName = "TV Drama" });
        }
    }
}
