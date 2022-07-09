using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MovieMVC.Data.EntityConfiguration
{
    public class RoleEntityConfig : IEntityTypeConfiguration<IdentityRole>
    {
        private string[] _roleId;

        public RoleEntityConfig(string[] roleId)
        {
            _roleId = roleId;
        }

        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = _roleId[0],
                    Name = "Guest",
                    NormalizedName = "GUEST"
                },
                new IdentityRole
                {
                    Id = _roleId[1],
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                }
             );
        }
    }
}
