using Microsoft.AspNetCore.Identity;
using RatedTravel.Data.DataModels;
using static RatedTravel.Common.GeneralApplicationConstants;

namespace RatedTravel.App.Web.Infrastructure
{
    public static class ApplicationBuilderExtensions 
    {
        //Seed the user with admin role
        public static IApplicationBuilder SeedAdministrator(this IApplicationBuilder app, string email)
        {
            using IServiceScope scopedServices = app.ApplicationServices.CreateScope();

            IServiceProvider serviceProvider = scopedServices.ServiceProvider;

            UserManager<ApplicationUser> userManager =
                serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager<IdentityRole<Guid>> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            Task.Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdminRoleName))
                    {
                        return;
                    }

                    IdentityRole<Guid> role =
                        new IdentityRole<Guid>(AdminRoleName);

                    await roleManager.CreateAsync(role);

                    ApplicationUser adminUser =
                        await userManager.FindByEmailAsync(email);

                    await userManager.AddToRoleAsync(adminUser, AdminRoleName);
                })
                .GetAwaiter()
                .GetResult();

            return app;
        }

      
    }
}
