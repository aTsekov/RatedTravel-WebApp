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
        }
    }
}
