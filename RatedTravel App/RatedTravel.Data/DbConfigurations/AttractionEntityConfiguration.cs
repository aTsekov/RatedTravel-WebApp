using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RatedTravel.Data.DataModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RatedTravel.Data.DbConfigurations
{
    public class AttractionEntityConfiguration : IEntityTypeConfiguration<Attraction>

    {

        public void Configure(EntityTypeBuilder<Attraction> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(c => c.IsActive).HasDefaultValue(true);
            builder.HasOne(a => a.City)
                .WithMany(c => c.Attractions)
                .HasForeignKey(a => a.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new Attraction
                {
                    Id = 1,
                    Name = "Historical Museum",
                    Description = "Explore the rich history of our city at the Historical Museum.",
                    ImageUrl = "HistoricalMuseumLondon.jpg",
                    WorthVisitingScore = 5,
                    IsActive = true,
                    AppUserId = 1,
                    CityId = 1
                },
                new Attraction
                {
                    Id = 2,
                    Name = "Botanical Garden",
                    Description = "Immerse yourself in the beauty of nature at our Botanical Garden.",
                    ImageUrl = "BotanicalGardenParis.jpg",
                    WorthVisitingScore = 4,
                    IsActive = true,
                    AppUserId = 1,
                    CityId = 2
                }
            );
        }
    }
}
