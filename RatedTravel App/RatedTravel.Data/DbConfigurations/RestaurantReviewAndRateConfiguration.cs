using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RatedTravel.Data.DataModels;

namespace RatedTravel.Data.DbConfigurations
{
    public class RestaurantReviewAndRateConfiguration : IEntityTypeConfiguration<RestaurantReviewAndRate>
    {
        public void Configure(EntityTypeBuilder<RestaurantReviewAndRate> builder)
        {
            builder.HasKey(brr => brr.Id);
            builder.Property(c => c.IsActive).HasDefaultValue(true);
            builder.HasOne(r => r.Restaurant)
                .WithMany(r => r.Reviews)
                .HasForeignKey(r => r.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new RestaurantReviewAndRate
                {
                    Id = 1,
                    ReviewText = "Great food and excellent service!",
                    LocationRate = 4,
                    FoodRate = 5,
                    PriceRate = 3,
                    ServiceRate = 4,
                    IsActive = true,
                    RestaurantId = 1
                },
                new RestaurantReviewAndRate
                {
                    Id = 2,
                    ReviewText = "Average food quality but the ambiance is nice.",
                    LocationRate = 3,
                    FoodRate = 3,
                    PriceRate = 4,
                    ServiceRate = 4,
                    IsActive = true,
                    RestaurantId = 2
                }
            );
        }
    }
}
