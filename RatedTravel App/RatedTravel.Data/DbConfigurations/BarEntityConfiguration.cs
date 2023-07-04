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
    public class BarEntityConfiguration : IEntityTypeConfiguration<Bar>
    {
        public void Configure(EntityTypeBuilder<Bar> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(c => c.IsActive).HasDefaultValue(true);

            // Restrict the OnDelete behavior
            builder.HasMany(b => b.Reviews)
                .WithOne(r => r.Bar)
                .HasForeignKey(r => r.BarId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new Bar
                {
                    Id = 1,
                    Name = "The Pub",
                    Address = "123 Main Street",
                    ImageUrl = "ThePubLondon.jpg",
                    Description = "A cozy pub with a wide selection of beers.",
                    OverallScore = 4,
                    Website = "https://www.londonpub.imperialhotels.co.uk",
                    IsActive = true,
                    EmployeeId = 1,
                    AppUserId = 1,
                    CityId = 1
                },
                new Bar
                {
                    Id = 2,
                    Name = "Cheers Bar",
                    Address = "456 Elm Street",
                    ImageUrl = "ParisBar.jpg",
                    Description = "A popular bar known for its friendly atmosphere.",
                    OverallScore = 4,
                    Website = "https://lecalbarcocktail.com/",
                    IsActive = true,
                    AppUserId = 1,
                    CityId = 2
                }
            );
        }
    }
}
