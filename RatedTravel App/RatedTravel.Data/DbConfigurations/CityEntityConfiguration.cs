using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatedTravel.Data.DataModels;

namespace RatedTravel.Data.DbConfigurations
{
    public class CityEntityConfiguration :IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
           
            builder.Property(c => c.IsActive).HasDefaultValue(true);

            // Restrict the OnDelete behavior
            builder.HasMany(c => c.Bars)
                .WithOne(b => b.City)
                .HasForeignKey(b => b.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Restaurants)
                .WithOne(r => r.City)
                .HasForeignKey(r => r.CityId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(c => c.Attractions)
                .WithOne(r => r.City)
                .HasForeignKey(r => r.CityId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.Employee)
                .WithMany(c => c.OwnedCities)
                .HasForeignKey(c => c.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasData(
                    new City
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
                    },
                    new City
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
                    }

            );




        }
    }
}
