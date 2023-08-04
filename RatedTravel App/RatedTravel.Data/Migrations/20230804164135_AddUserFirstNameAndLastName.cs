using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RatedTravel.Data.Migrations
{
    public partial class AddUserFirstNameAndLastName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "Test");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "Testov");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("7e980128-41f1-4351-b11f-2e9ac6d0cade"),
                columns: new[] { "NightlifeScore", "TransportScore" },
                values: new object[] { 4, 5 });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("ca551b7b-d085-45e5-b26d-f62b7d6965ee"),
                columns: new[] { "NightlifeScore", "TransportScore" },
                values: new object[] { 4, 4 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("7e980128-41f1-4351-b11f-2e9ac6d0cade"),
                columns: new[] { "NightlifeScore", "TransportScore" },
                values: new object[] { 8, 9 });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("ca551b7b-d085-45e5-b26d-f62b7d6965ee"),
                columns: new[] { "NightlifeScore", "TransportScore" },
                values: new object[] { 9, 8 });
        }
    }
}
