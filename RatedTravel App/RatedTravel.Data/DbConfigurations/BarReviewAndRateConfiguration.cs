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
        }
    }
}
