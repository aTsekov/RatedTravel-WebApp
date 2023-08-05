using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RatedTravel.Data.Migrations
{
    public partial class ChangeNamesOfSeededUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("75339214-cfa7-4006-9696-10fbe87f3039"),
                columns: new[] { "FirstName", "LastName" },
                values: new object[] { "Pesho", "Peshov" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d6eb8c37-86bc-423a-ac69-b98d16b0a887"),
                columns: new[] { "FirstName", "LastName" },
                values: new object[] { "Antoni", "Tsekov" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("75339214-cfa7-4006-9696-10fbe87f3039"),
                columns: new[] { "FirstName", "LastName" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d6eb8c37-86bc-423a-ac69-b98d16b0a887"),
                columns: new[] { "FirstName", "LastName" },
                values: new object[] { null, null });
        }
    }
}
