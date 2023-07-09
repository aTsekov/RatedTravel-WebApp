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
    public class BarReviewAndRateConfiguration : IEntityTypeConfiguration<BarReviewAndRate>
    {
        public void Configure(EntityTypeBuilder<BarReviewAndRate> builder)
        {
            builder.HasKey(brr => brr.Id);
            builder.Property(c => c.IsActive).HasDefaultValue(true);
            builder.HasOne(brr => brr.Bar)
                .WithMany(b => b.Reviews)
                .HasForeignKey(brr => brr.BarId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.HasData(
            //    new BarReviewAndRate
            //    {
            //        Id = 1,
            //        ReviewText = "Great atmosphere and friendly staff!",
            //        LocationRate = 4,
            //        PriceRate = 3,
            //        ServiceRate = 5,
            //        MusicRate = 4,
            //        IsActive = true,
            //        BarId = 1
            //    },
            //    new BarReviewAndRate
            //    {
            //        Id = 2,
            //        ReviewText = "Lively place with good music, but drinks are a bit overpriced.",
            //        LocationRate = 3,
            //        PriceRate = 2,
            //        ServiceRate = 4,
            //        MusicRate = 5,
            //        IsActive = true,
            //        BarId = 2
            //    }
            //);
        }
    }
}
