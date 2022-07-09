using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieMVC.Models;
using MovieMVC.Data.EntityConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MovieMVC.Data
{
    public class MovieContext:IdentityDbContext<User>
    {
        public MovieContext (DbContextOptions<MovieContext> options): base(options)
        {}

        public DbSet<Category> Categories { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            string[] roleId = { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };
            string[] userId = { Guid.NewGuid().ToString() };

            modelBuilder.ApplyConfiguration(new MovieEntityConfig());
            modelBuilder.ApplyConfiguration(new CategoryEntityConfig());
            modelBuilder.ApplyConfiguration(new GenreEntityConfig());
            modelBuilder.ApplyConfiguration(new RoleEntityConfig(roleId));
            modelBuilder.ApplyConfiguration(new UserEntityConfig(userId));

            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasData(
                new IdentityUserRole<string>
                {
                    RoleId = roleId[1],
                    UserId = userId[0]
                });
        }
    }
}
