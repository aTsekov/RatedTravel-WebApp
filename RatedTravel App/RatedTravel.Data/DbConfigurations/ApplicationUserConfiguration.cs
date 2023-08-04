using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RatedTravel.Data.DataModels;
using Microsoft.EntityFrameworkCore;

namespace RatedTravel.Data.DbConfigurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder) 
        {
            builder.Property( u => u.FirstName).HasDefaultValue("Test");
            builder.Property( u => u.LastName).HasDefaultValue("Testov");

            builder.HasData(
                new ApplicationUser()
                {
                    Id = Guid.Parse("75339214-CFA7-4006-9696-10FBE87F3039"),
                    UserName = "pesho@abv.bg",
                    NormalizedUserName = "PESHO@ABV.BG",
                    Email= "pesho@abv.bg",
                    NormalizedEmail = "PESHO@ABV.BG",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAEAACcQAAAAEDnxpjlTaYJ1vX4v7J12oUBUTycBNDLVyZWjWG2p6MzqoratcAY+bidSg8Rxt+glWg==",
                    SecurityStamp = "IO4GJSB3O2UU22LJ737SOGOVYZM3PM2Z",
                    ConcurrencyStamp = "0914fbf4-4cd3-4952-95d9-f724d0ccc986",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0

                },
                new ApplicationUser()
                {
                    Id = Guid.Parse("D6EB8C37-86BC-423A-AC69-B98D16B0A887"),
                    UserName = "antk@abv.bg",
                    NormalizedUserName = "ANTK@ABV.BG",
                    Email = "antk@abv.bg",
                    NormalizedEmail = "ANTK@ABV.BG",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAEAACcQAAAAEJYaHDKeWygEQcg2rAHKDlGZiPXR8dhgrXUME+kIp6xYI4DKTpSznlovkmsGo3yYeA==",
                    SecurityStamp = "WPQQQTDY45QTNVNFPBU7CTFVSD5A4T2V",
                    ConcurrencyStamp = "b7cf787d-bb27-4d2f-9c91-6b7fdb4c70ea",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0

                }
            );
        }
    }
}
