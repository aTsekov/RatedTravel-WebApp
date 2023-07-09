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

            //builder.HasData(
            //    new Restaurant
            //    {
            //        Id = 1,
            //        Name = "The Bistro",
            //        ImageUrl = "BistroLondon.jpg",
            //        Address = "789 Oak Street",
            //        Description = "A charming bistro offering delicious cuisine.",
            //        OverallScore = 4,
            //        IsActive = true,
            //        EmployeeId = 1,
            //        AppUserId = 1,
            //        CityId = 1
            //    },
            //    new Restaurant
            //    {
            //        Id = 2,
            //        Name = "La Trattoria",
            //        ImageUrl = "LaTrattoria.jpg",
            //        Address = "321 Pine Street",
            //        Description = "An Italian restaurant known for its authentic dishes.",
            //        OverallScore = 4,
            //        IsActive = true,
            //        AppUserId = 1,
            //        CityId = 2
            //    }
            //);
        }
    }
}
