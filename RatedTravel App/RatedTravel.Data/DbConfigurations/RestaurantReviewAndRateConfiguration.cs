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
        }
    }
}
