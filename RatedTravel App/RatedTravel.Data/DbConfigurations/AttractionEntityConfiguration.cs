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
                    UserId = Guid.Parse("75339214-CFA7-4006-9696-10FBE87F3039"),
                    CityId = Guid.Parse("7E980128-41F1-4351-B11F-2E9AC6D0CADE")
                },
                new Attraction
                {
                    Id = 2,
                    Name = "Botanical Garden",
                    Description = "Immerse yourself in the beauty of nature at our Botanical Garden.",
                    ImageUrl = "BotanicalGardenParis.jpg",
                    WorthVisitingScore = 4,
                    IsActive = true,
                    UserId = Guid.Parse("75339214-CFA7-4006-9696-10FBE87F3039"),
                    CityId = Guid.Parse("CA551B7B-D085-45E5-B26D-F62B7D6965EE")
                }
            );
        }
    }
}
