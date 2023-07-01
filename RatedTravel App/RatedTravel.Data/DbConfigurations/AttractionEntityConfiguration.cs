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
        }
    }
}
