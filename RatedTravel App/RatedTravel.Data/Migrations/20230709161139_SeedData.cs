using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RatedTravel.Data.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("75339214-cfa7-4006-9696-10fbe87f3039"), 0, "0914fbf4-4cd3-4952-95d9-f724d0ccc986", "pesho@abv.bg", false, true, null, "PESHO@ABV.BG", "PESHO@ABV.BG", "AQAAAAEAACcQAAAAEDnxpjlTaYJ1vX4v7J12oUBUTycBNDLVyZWjWG2p6MzqoratcAY+bidSg8Rxt+glWg==", null, false, "IO4GJSB3O2UU22LJ737SOGOVYZM3PM2Z", false, "pesho@abv.bg" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("d6eb8c37-86bc-423a-ac69-b98d16b0a887"), 0, "b7cf787d-bb27-4d2f-9c91-6b7fdb4c70ea", "antk@abv.bg", false, true, null, "ANTK@ABV.BG", "ANTK@ABV.BG", "AQAAAAEAACcQAAAAEJYaHDKeWygEQcg2rAHKDlGZiPXR8dhgrXUME+kIp6xYI4DKTpSznlovkmsGo3yYeA==", null, false, "WPQQQTDY45QTNVNFPBU7CTFVSD5A4T2V", false, "antk@abv.bg" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FullName", "PhoneNumber", "UserId" },
                values: new object[] { new Guid("2d2ee1b2-d178-42b7-aebe-25f85f15902c"), "Antoni Tsekov", "1234567890", new Guid("d6eb8c37-86bc-423a-ac69-b98d16b0a887") });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Country", "Description", "EmployeeId", "ImageUrl", "IsActive", "Name", "NightlifeScore", "TransportScore", "UserId" },
                values: new object[] { new Guid("7e980128-41f1-4351-b11f-2e9ac6d0cade"), "United Kingdom", "London is the capital and largest city of England and the United Kingdom. It is a vibrant and diverse city known for its rich history, iconic landmarks, and cultural attractions. From the majestic Tower of London to the bustling streets of Covent Garden, there is something for everyone in this cosmopolitan metropolis.", new Guid("2d2ee1b2-d178-42b7-aebe-25f85f15902c"), "London.jpg", true, "London", 8, 9, null });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Country", "Description", "EmployeeId", "ImageUrl", "IsActive", "Name", "NightlifeScore", "TransportScore", "UserId" },
                values: new object[] { new Guid("ca551b7b-d085-45e5-b26d-f62b7d6965ee"), "France", "Paris, the capital of France, is a city renowned for its art, fashion, and cuisine. With its world-famous landmarks like the Eiffel Tower, Louvre Museum, and Notre-Dame Cathedral, Paris attracts millions of visitors each year. The city's charming streets, sidewalk cafes, and romantic atmosphere make it a favorite destination for couples and art enthusiasts.", new Guid("2d2ee1b2-d178-42b7-aebe-25f85f15902c"), "Paris.jpg", true, "Paris", 9, 8, null });

            migrationBuilder.InsertData(
                table: "Attractions",
                columns: new[] { "Id", "CityId", "Description", "EmployeeId", "ImageUrl", "IsActive", "Name", "UserId", "WorthVisitingScore" },
                values: new object[,]
                {
                    { 1, new Guid("7e980128-41f1-4351-b11f-2e9ac6d0cade"), "Explore the rich history of our city at the Historical Museum.", null, "HistoricalMuseumLondon.jpg", true, "Historical Museum", new Guid("75339214-cfa7-4006-9696-10fbe87f3039"), 5 },
                    { 2, new Guid("ca551b7b-d085-45e5-b26d-f62b7d6965ee"), "Immerse yourself in the beauty of nature at our Botanical Garden.", null, "BotanicalGardenParis.jpg", true, "Botanical Garden", new Guid("75339214-cfa7-4006-9696-10fbe87f3039"), 4 }
                });

            migrationBuilder.InsertData(
                table: "Bars",
                columns: new[] { "Id", "Address", "CityId", "Description", "EmployeeId", "ImageUrl", "IsActive", "Name", "OverallScore", "UserId", "Website" },
                values: new object[,]
                {
                    { 1, "123 Main Street", new Guid("7e980128-41f1-4351-b11f-2e9ac6d0cade"), "A cozy pub with a wide selection of beers.", null, "ThePubLondon.jpg", true, "The Pub", 4, new Guid("75339214-cfa7-4006-9696-10fbe87f3039"), "https://www.londonpub.imperialhotels.co.uk" },
                    { 2, "456 Elm Street", new Guid("ca551b7b-d085-45e5-b26d-f62b7d6965ee"), "A popular bar known for its friendly atmosphere.", null, "ParisBar.jpg", true, "Cheers Bar", 4, new Guid("75339214-cfa7-4006-9696-10fbe87f3039"), "https://lecalbarcocktail.com/" }
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "CityId", "Description", "EmployeeId", "ImageUrl", "IsActive", "Name", "OverallScore", "UserId" },
                values: new object[,]
                {
                    { 1, "789 Oak Street", new Guid("7e980128-41f1-4351-b11f-2e9ac6d0cade"), "A charming bistro offering delicious cuisine.", null, "BistroLondon.jpg", true, "The Bistro", 4, new Guid("75339214-cfa7-4006-9696-10fbe87f3039") },
                    { 2, "321 Pine Street", new Guid("ca551b7b-d085-45e5-b26d-f62b7d6965ee"), "An Italian restaurant known for its authentic dishes.", null, "LaTrattoria.jpg", true, "La Trattoria", 4, new Guid("75339214-cfa7-4006-9696-10fbe87f3039") }
                });

            migrationBuilder.InsertData(
                table: "BarReviewsAndRates",
                columns: new[] { "Id", "BarId", "IsActive", "LocationRate", "MusicRate", "PriceRate", "ReviewText", "ServiceRate" },
                values: new object[,]
                {
                    { 1, 1, true, 4, 4, 3, "Great atmosphere and friendly staff!", 5 },
                    { 2, 2, true, 3, 5, 2, "Lively place with good music, but drinks are a bit overpriced.", 4 }
                });

            migrationBuilder.InsertData(
                table: "RestaurantReviewsAndRates",
                columns: new[] { "Id", "FoodRate", "IsActive", "LocationRate", "PriceRate", "RestaurantId", "ReviewText", "ServiceRate" },
                values: new object[,]
                {
                    { 1, 5, true, 4, 3, 1, "Great food and excellent service!", 4 },
                    { 2, 3, true, 3, 4, 2, "Average food quality but the ambiance is nice.", 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Attractions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BarReviewsAndRates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BarReviewsAndRates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RestaurantReviewsAndRates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RestaurantReviewsAndRates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Bars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bars",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("75339214-cfa7-4006-9696-10fbe87f3039"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("7e980128-41f1-4351-b11f-2e9ac6d0cade"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("ca551b7b-d085-45e5-b26d-f62b7d6965ee"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("2d2ee1b2-d178-42b7-aebe-25f85f15902c"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d6eb8c37-86bc-423a-ac69-b98d16b0a887"));
        }
    }
}
