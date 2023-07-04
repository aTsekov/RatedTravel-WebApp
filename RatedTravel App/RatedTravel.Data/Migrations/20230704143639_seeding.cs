using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RatedTravel.Data.Migrations
{
    public partial class seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "AppUserId", "FullName", "PhoneNumber", "UserId" },
                values: new object[] { 1, null, "Antoni Tsekov", "1234567890", 1 });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "AppUserId", "AppUserId1", "Country", "Description", "EmployeeId", "ImageUrl", "IsActive", "Name", "NightlifeScore", "TransportScore" },
                values: new object[] { 1, null, null, "United Kingdom", "London is the capital and largest city of England and the United Kingdom. It is a vibrant and diverse city known for its rich history, iconic landmarks, and cultural attractions. From the majestic Tower of London to the bustling streets of Covent Garden, there is something for everyone in this cosmopolitan metropolis.", 1, "London.jpg", true, "London", 8, 9 });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "AppUserId", "AppUserId1", "Country", "Description", "EmployeeId", "ImageUrl", "IsActive", "Name", "NightlifeScore", "TransportScore" },
                values: new object[] { 2, null, null, "France", "Paris, the capital of France, is a city renowned for its art, fashion, and cuisine. With its world-famous landmarks like the Eiffel Tower, Louvre Museum, and Notre-Dame Cathedral, Paris attracts millions of visitors each year. The city's charming streets, sidewalk cafes, and romantic atmosphere make it a favorite destination for couples and art enthusiasts.", 1, "Paris.jpg", true, "Paris", 9, 8 });

            migrationBuilder.InsertData(
                table: "Attractions",
                columns: new[] { "Id", "AppUserId", "AppUserId1", "CityId", "Description", "EmployeeId", "ImageUrl", "IsActive", "Name", "WorthVisitingScore" },
                values: new object[,]
                {
                    { 1, 1, null, 1, "Explore the rich history of our city at the Historical Museum.", null, "HistoricalMuseumLondon.jpg", true, "Historical Museum", 5 },
                    { 2, 1, null, 2, "Immerse yourself in the beauty of nature at our Botanical Garden.", null, "BotanicalGardenParis.jpg", true, "Botanical Garden", 4 }
                });

            migrationBuilder.InsertData(
                table: "Bars",
                columns: new[] { "Id", "Address", "AppUserId", "AppUserId1", "CityId", "Description", "EmployeeId", "ImageUrl", "IsActive", "Name", "OverallScore", "Website" },
                values: new object[,]
                {
                    { 1, "123 Main Street", 1, null, 1, "A cozy pub with a wide selection of beers.", 1, "ThePubLondon.jpg", true, "The Pub", 4, "https://www.londonpub.imperialhotels.co.uk" },
                    { 2, "456 Elm Street", 1, null, 2, "A popular bar known for its friendly atmosphere.", null, "ParisBar.jpg", true, "Cheers Bar", 4, "https://lecalbarcocktail.com/" }
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "AppUserId", "AppUserId1", "CityId", "Description", "EmployeeId", "ImageUrl", "IsActive", "Name", "OverallScore" },
                values: new object[,]
                {
                    { 1, "789 Oak Street", 1, null, 1, "A charming bistro offering delicious cuisine.", 1, "BistroLondon.jpg", true, "The Bistro", 4 },
                    { 2, "321 Pine Street", 1, null, 2, "An Italian restaurant known for its authentic dishes.", null, "LaTrattoria.jpg", true, "La Trattoria", 4 }
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
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
