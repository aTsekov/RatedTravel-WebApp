using System;
using System.Collections.Generic;
using System.Linq;
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
        }
    }
}
