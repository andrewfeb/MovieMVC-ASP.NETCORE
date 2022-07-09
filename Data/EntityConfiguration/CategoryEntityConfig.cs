using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieMVC.Models;

namespace MovieMVC.Data.EntityConfiguration
{
    public class CategoryEntityConfig:IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.CategoryName)
                .HasColumnType("varchar(30)")
                .IsRequired();

            // Data Seeding
            builder.HasData(new { Id = 1, CategoryName = "TV Drama" });
        }
    }
}
