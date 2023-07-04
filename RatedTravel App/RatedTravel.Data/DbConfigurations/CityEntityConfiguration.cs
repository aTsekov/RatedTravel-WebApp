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
            builder.HasData(
                    new City
                    {
                        Id = 1,
                        Name = "London",
                        Country = "United Kingdom",
                        Description = "London is the capital and largest city of England and the United Kingdom. It is a vibrant and diverse city known for its rich history, iconic landmarks, and cultural attractions. From the majestic Tower of London to the bustling streets of Covent Garden, there is something for everyone in this cosmopolitan metropolis.",
                        ImageUrl = "London.jpg",
                        NightlifeScore = 8,
                        TransportScore = 9,
                        IsActive = true,
                        EmployeeId = 1
                    },
                    new City
                    {
                        Id = 2,
                        Name = "Paris",
                        Country = "France",
                        Description = "Paris, the capital of France, is a city renowned for its art, fashion, and cuisine. With its world-famous landmarks like the Eiffel Tower, Louvre Museum, and Notre-Dame Cathedral, Paris attracts millions of visitors each year. The city's charming streets, sidewalk cafes, and romantic atmosphere make it a favorite destination for couples and art enthusiasts.",
                        ImageUrl = "Paris.jpg",
                        NightlifeScore = 9,
                        TransportScore = 8,
                        IsActive = true,
                        EmployeeId = 1
                    }
                   
            );




        }
    }
}
