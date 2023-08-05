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
    public class EmployeeEntityConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData(
                new Employee
                {
                    Id = Guid.Parse("2D2EE1B2-D178-42B7-AEBE-25F85F15902C"),
                    FullName = "Antoni Tsekov",
                    PhoneNumber = "1234567890",
                    UserId = Guid.Parse("D6EB8C37-86BC-423A-AC69-B98D16B0A887")
                },
                new Employee
                { //THIS IS THE ADMIN USER
                    Id = Guid.Parse("148FD70C-8A0A-4C13-AF75-FFC5F204A0AC"),
                    FullName = "Admin Adminov",
                    PhoneNumber = "111222333",
                    UserId = Guid.Parse("1CFE52CB-AFC4-4FFA-A1CC-236DC7AE148F")
                }
            );
        }
    }
}
