using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RatedTravel.Core.Interfaces;
using RatedTravel.Core.Services;
using RatedTravel.Data;
using RatedTravel.Data.DataModels;
using RaterTravel.Common;


namespace RatedTravel.Web
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
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = GeneralApplicationConstants.MinPasswordLength;
                }).AddEntityFrameworkStores<RatedTravelDbContext>();

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