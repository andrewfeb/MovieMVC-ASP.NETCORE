using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieMVC.Models;

namespace MovieMVC.Data.EntityConfiguration
{
    public class UserEntityConfig : IEntityTypeConfiguration<User>
    {
        private string[] _userId;

        public UserEntityConfig(string[] userId)
        {
            _userId = userId;
        }

        public void Configure(EntityTypeBuilder<User> builder)
        {
            User user = new User
            {
                Id = _userId[0],
                Name = "Administrator",
                UserName = "administrator",
                NormalizedUserName = "administrator",
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com"
            };

            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

            user.PasswordHash = passwordHasher.HashPassword(user, "admin@123");

            builder.HasData(user);
        }
    }
}
