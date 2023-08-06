using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RatedTravel.Data;
using RatedTravel.Data.DataModels;

namespace RatedTravel.Tests
{
    public static class DatabaseSeeder
    {
        public static ApplicationUser ToBecomeEmployee = null!;
        public static ApplicationUser NormalUser = null!;
        public static Employee MyEmployee = null!;
        public static Bar Bar1 = null!;
        public static Bar Bar2 = null!;
        public static Attraction Attraction1 = null!;
        public static Attraction Attraction2;
        public static Restaurant Restaurant1 = null!;
        public static Restaurant Restaurant2 = null!;
        public static BarReviewAndRate BarReview1 = null!;
        public static BarReviewAndRate BarReview2 = null!;
        public static RestaurantReviewAndRate RestaurantReview1 = null!;
        public static RestaurantReviewAndRate RestaurantReview2 = null!;
        public static City City1 = null!;
        public static City City2 = null!;

        public static List<City> Cities = new List<City>()
        {
            City1,
            City2
        };

        public static void SeedDatabase(RatedTravelDbContext dbContext)
        {
            ToBecomeEmployee = new ApplicationUser()
            {
                Id = Guid.Parse("D1EB8C37-86BC-423A-AC69-B98D16B0A887"),
                UserName = "antk@abv.bg",
                NormalizedUserName = "ANTK@ABV.BG",
                Email = "antk@abv.bg",
                FirstName = "Olga",
                LastName = "Tsekova",
                NormalizedEmail = "ANTK@ABV.BG",
                EmailConfirmed = false,
                PasswordHash =
                     "AQAAAAEAACcQAAAAEJYaHDKeWygEQcg2rAHKDlGZiPXR8dhgrXUME+kIp6xYI4DKTpSznlovkmsGo3yYeA==",
                SecurityStamp = "WPQQQTDY45QTNVNFPBU7CTFVSD5A4T2V",
                ConcurrencyStamp = "b7cf787d-bb27-4d2f-9c91-6b7fdb4c70ea",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };
            NormalUser = new ApplicationUser()
            {
                Id = Guid.Parse("75339214-CFA7-4006-9696-10FBE87F3039"),
                UserName = "pesho@abv.bg",
                FirstName = "Pesho",
                LastName = "Peshov",
                NormalizedUserName = "PESHO@ABV.BG",
                Email = "pesho@abv.bg",
                NormalizedEmail = "PESHO@ABV.BG",
                EmailConfirmed = false,
                PasswordHash =
                    "AQAAAAEAACcQAAAAEDnxpjlTaYJ1vX4v7J12oUBUTycBNDLVyZWjWG2p6MzqoratcAY+bidSg8Rxt+glWg==",
                SecurityStamp = "IO4GJSB3O2UU22LJ737SOGOVYZM3PM2Z",
                ConcurrencyStamp = "0914fbf4-4cd3-4952-95d9-f724d0ccc986",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };
            MyEmployee = new Employee()
            {
                Id = Guid.Parse("2D2EE1B2-D178-42B7-AEBE-25F85F15902C"),
                FullName = "Antoni Tsekov",
                PhoneNumber = "1234567890",
                UserId = Guid.Parse("D6EB8C37-86BC-423A-AC69-B98D16B0A887")
            };
            City1 = new City
            {
                Id = Guid.Parse("7E980128-41F1-4351-B11F-2E9AC6D0CADE"),
                Name = "London",
                Country = "United Kingdom",
                Description = "London is the capital and largest city of England and the United Kingdom. It is a vibrant and diverse city known for its rich history, iconic landmarks, and cultural attractions. From the majestic Tower of London to the bustling streets of Covent Garden, there is something for everyone in this cosmopolitan metropolis.",
                ImageUrl = "London.jpg",
                NightlifeScore = 4,
                TransportScore = 5,
                IsActive = true,
                EmployeeId = Guid.Parse("2D2EE1B2-D178-42B7-AEBE-25F85F15902C")
            };
            City2 = new City
            {
                Id = Guid.Parse("CA551B7B-D085-45E5-B26D-F62B7D6965EE"),
                Name = "Paris",
                Country = "France",
                Description = "Paris, the capital of France, is a city renowned for its art, fashion, and cuisine. With its world-famous landmarks like the Eiffel Tower, Louvre Museum, and Notre-Dame Cathedral, Paris attracts millions of visitors each year. The city's charming streets, sidewalk cafes, and romantic atmosphere make it a favorite destination for couples and art enthusiasts.",
                ImageUrl = "Paris.jpg",
                NightlifeScore = 4,
                TransportScore = 4,
                IsActive = true,
                EmployeeId = Guid.Parse("2D2EE1B2-D178-42B7-AEBE-25F85F15902C")
            };
            

            Bar1 = new Bar
            {
                Id = 1,
                Name = "The Pub",
                Address = "123 Main Street",
                ImageUrl = "ThePubLondon.jpg",
                Description = "A cozy pub with a wide selection of beers.",
                OverallScore = 4,
                Website = "https://www.londonpub.imperialhotels.co.uk",
                IsActive = true,
                UserId = Guid.Parse("D6EB8C37-86BC-423A-AC69-B98D16B0A887"),
                CityId = Guid.Parse("7E980128-41F1-4351-B11F-2E9AC6D0CADE")
            };
            Bar2 = new Bar
            {
                Id = 2,
                Name = "Cheers Bar",
                Address = "456 Elm Street",
                ImageUrl = "ParisBar.jpg",
                Description = "A popular bar known for its friendly atmosphere.",
                OverallScore = 4,
                Website = "https://lecalbarcocktail.com/",
                IsActive = true,
                UserId = Guid.Parse("D6EB8C37-86BC-423A-AC69-B98D16B0A887"),
                CityId = Guid.Parse("CA551B7B-D085-45E5-B26D-F62B7D6965EE")
            };
            

            Attraction1 = new Attraction
            {
                Id = 3,
                Name = "Historical Museum",
                Description = "Explore the rich history of our city at the Historical Museum.",
                ImageUrl = "HistoricalMuseumLondon.jpg",
                WorthVisitingScore = 5,
                IsActive = true,
                UserId = Guid.Parse("D6EB8C37-86BC-423A-AC69-B98D16B0A887"),
                CityId = Guid.Parse("7E980128-41F1-4351-B11F-2E9AC6D0CADE")
            };
            Attraction2 = new Attraction
            {
                Id = 4,
                Name = "Botanical Garden",
                Description = "Immerse yourself in the beauty of nature at our Botanical Garden.",
                ImageUrl = "BotanicalGardenParis.jpg",
                WorthVisitingScore = 4,
                IsActive = true,
                UserId = Guid.Parse("D6EB8C37-86BC-423A-AC69-B98D16B0A887"),
                CityId = Guid.Parse("CA551B7B-D085-45E5-B26D-F62B7D6965EE")
            };
           

            Restaurant1 = new Restaurant
            {
                Id = 5,
                Name = "The Bistro",
                ImageUrl = "BistroLondon.jpg",
                Address = "789 Oak Street",
                Description = "A charming bistro offering delicious cuisine.",
                OverallScore = 4,
                IsActive = true,
                UserId = Guid.Parse("D6EB8C37-86BC-423A-AC69-B98D16B0A887"),
                CityId = Guid.Parse("7E980128-41F1-4351-B11F-2E9AC6D0CADE")
            };
            Restaurant2 = new Restaurant
            {
                Id = 6,
                Name = "La Trattoria",
                ImageUrl = "LaTrattoria.jpg",
                Address = "321 Pine Street",
                Description = "An Italian restaurant known for its authentic dishes.",
                OverallScore = 4,
                IsActive = true,
                UserId = Guid.Parse("D6EB8C37-86BC-423A-AC69-B98D16B0A887"),
                CityId = Guid.Parse("CA551B7B-D085-45E5-B26D-F62B7D6965EE")
            };
           

            BarReview1 = new BarReviewAndRate
            {
                Id = 7,
                ReviewText = "Great atmosphere and friendly staff!",
                LocationRate = 4,
                PriceRate = 3,
                ServiceRate = 5,
                MusicRate = 4,
                IsActive = true,
                BarId = 2
            };
            BarReview2 = new BarReviewAndRate
            {
                Id = 1,
                ReviewText = "Lively place with good music, but drinks are a bit overpriced.",
                LocationRate = 3,
                PriceRate = 2,
                ServiceRate = 4,
                MusicRate = 5,
                IsActive = true,
                BarId = 2
            };
           

            RestaurantReview1 = new RestaurantReviewAndRate
            {
                Id = 2,
                ReviewText = "Great food and excellent service!",
                LocationRate = 4,
                FoodRate = 5,
                PriceRate = 3,
                ServiceRate = 4,
                IsActive = true,
                RestaurantId = 6
            };
            RestaurantReview2 = new RestaurantReviewAndRate
            {
                Id = 5,
                ReviewText = "Average food quality but the ambiance is nice.",
                LocationRate = 3,
                FoodRate = 3,
                PriceRate = 4,
                ServiceRate = 4,
                IsActive = true,
                RestaurantId = 6
            };

            dbContext.Users.Add(ToBecomeEmployee);
            dbContext.Users.Add(NormalUser);
            dbContext.Employees.Add(MyEmployee);
            dbContext.Cities.AddRange(City1, City2);
            dbContext.Bars.AddRange(Bar1, Bar2);
            dbContext.Attractions.AddRange(Attraction1, Attraction2);
            dbContext.Restaurants.AddRange(Restaurant1, Restaurant2);
            dbContext.BarReviewsAndRates.AddRange(BarReview1, BarReview2);
            

        }
    }
}
