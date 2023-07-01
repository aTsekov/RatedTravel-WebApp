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
    public class RestaurantEntityConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(c => c.IsActive).HasDefaultValue(true);

            // Restrict the OnDelete behavior
            builder.HasMany(r => r.Reviews)
                .WithOne(rr => rr.Restaurant)
                .HasForeignKey(rr => rr.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
