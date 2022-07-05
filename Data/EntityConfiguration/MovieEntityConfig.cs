using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieMVC.Models;

namespace MovieMVC.Data.EntityConfiguration
{
    public class MovieEntityConfig : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.Property(m => m.Title)
                .HasColumnType("varchar(30)")
                .IsRequired();

            builder.Property(m => m.Year)
                .HasColumnType("char(4)");

            builder.Property(m => m.Cover)
                .HasColumnType("varchar(125)");

            // relationship one to many between movie and category
            builder.HasOne(m => m.Category)
                .WithMany(c => c.Movies)
                .HasForeignKey(m => m.CategoryID);

            // relationship many to many between movie and genre            
            
        }
    }
}
