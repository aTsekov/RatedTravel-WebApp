using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RatedTravel.Data.Migrations
{
    public partial class SeedAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("1cfe52cb-afc4-4ffa-a1cc-236dc7ae148f"), 0, "94bd2382-035e-4d18-9d69-ac4bff1cea38", "admin@abv.bg", false, "Admin", "Adminov", true, null, "ADMIN@ABV.BG", "ADMIN@ABV.BG", "AQAAAAEAACcQAAAAEIXAEj/zxVPqj6r1ZB00uJ1DMrDd4h9I/4XDTYN+DQP7mwhf5d+EDNcDArbM3pYT5w==", null, false, "S5MQMIEVUDPTHQXJMACG5CPC6ZCJI6H4", false, "admin@abv.bg" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FullName", "PhoneNumber", "UserId" },
                values: new object[] { new Guid("148fd70c-8a0a-4c13-af75-ffc5f204a0ac"), "Admin Adminov", "111222333", new Guid("1cfe52cb-afc4-4ffa-a1cc-236dc7ae148f") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("148fd70c-8a0a-4c13-af75-ffc5f204a0ac"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1cfe52cb-afc4-4ffa-a1cc-236dc7ae148f"));
        }
    }
}
