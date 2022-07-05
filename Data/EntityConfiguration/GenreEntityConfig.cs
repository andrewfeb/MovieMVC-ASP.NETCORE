using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieMVC.Models;

namespace MovieMVC.Data.EntityConfiguration
{
    public class GenreEntityConfig : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.Property(g => g.GenreName)
                .HasColumnType("varchar(30)")
                .IsRequired();
        }
    }
}
