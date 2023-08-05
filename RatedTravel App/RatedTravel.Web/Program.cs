using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RatedTravel.Core.Interfaces;
using RatedTravel.Core.Services;
using RatedTravel.Data;
using RatedTravel.Data.DataModels;
using static RaterTravel.Common.GeneralApplicationConstants;

using RatedTravel.App.Web.Infrastructure;

namespace RatedTravel.App.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<RatedTravelDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount =
                        builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
                    options.Password.RequireLowercase =
                        builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
                    options.Password.RequireUppercase =
                        builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
                    options.Password.RequireNonAlphanumeric =
                        builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
                    options.Password.RequiredLength =
                        builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
                })
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<RatedTravelDbContext>();

            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<ICityService, CityService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IRestaurantService, RestaurantService>();
            builder.Services.AddScoped<IBarService, BarService>();
            builder.Services.AddScoped<IUserService, UserService>();

            

            builder.Services.ConfigureApplicationCookie(options=>
            {
                options.LoginPath = "/User/Login";
                options.LogoutPath = "/User/Logout";
                
            });

            //With this we are secured against CSRF attacks
            builder.Services.AddControllersWithViews(options =>
            {
				options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
			});

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseDeveloperExceptionPage(); // When we encounter an error we would get more information in the browser
            }
            else
            {
                app.UseExceptionHandler("/Home/Error/500");
                app.UseStatusCodePagesWithRedirects("Home/Error?statusCode={0}");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            if (app.Environment.IsDevelopment())
            {
                app.SeedAdministrator(AdminEmail);
            }

            app.UseEndpoints(config =>
            {
	            config.MapControllerRoute(
		            name: "areas",
		            pattern: "/{area:exists}/{controller=Home}/{action=Index}/{id?}"
	            );
				config.MapControllerRoute(
		            name: "default",
		            pattern: "{controller=Home}/{action=Index}/{id?}");
	            
	            config.MapRazorPages();
			});

            app.Run();
        }
    }
}