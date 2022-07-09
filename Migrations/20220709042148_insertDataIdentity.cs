using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieMVC.Migrations
{
    public partial class insertDataIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bdc8dd8e-bfa9-411f-8c42-7fdf7a74eac6", "73c5b75d-4e68-4c49-8f91-fa025cf9934b", "Guest", "GUEST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c8cbadaf-6a52-4329-96a2-a057fa572cfc", "addaeebc-23df-43c4-8244-b762d5c3c987", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d242b6f5-916e-4538-b594-dad2987e208b", 0, "74e08ebf-4a00-4679-994b-390644d82f0b", "admin@gmail.com", false, false, null, "Administrator", "admin@gmail.com", "administrator", "AQAAAAEAACcQAAAAEGR6/nE5np9buT2wlPXotPEH1soFIfP2YvUWj8Q9cFZ3/SkVgnW8xZCn6PudUoLUqQ==", null, false, "a3b1c0f8-7e5e-4a2c-99a2-bcde28382ecf", false, "administrator" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c8cbadaf-6a52-4329-96a2-a057fa572cfc", "d242b6f5-916e-4538-b594-dad2987e208b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bdc8dd8e-bfa9-411f-8c42-7fdf7a74eac6");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c8cbadaf-6a52-4329-96a2-a057fa572cfc", "d242b6f5-916e-4538-b594-dad2987e208b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8cbadaf-6a52-4329-96a2-a057fa572cfc");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d242b6f5-916e-4538-b594-dad2987e208b");
        }
    }
}
