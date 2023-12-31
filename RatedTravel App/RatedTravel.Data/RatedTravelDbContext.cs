﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RatedTravel.Data.DataModels;
using System.Reflection;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using RatedTravel.Data.DbConfigurations;

namespace RatedTravel.Data
{
    public class RatedTravelDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public RatedTravelDbContext(DbContextOptions<RatedTravelDbContext> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<Bar> Bars { get; set; } = null!;
        public DbSet<Restaurant> Restaurants { get; set; } = null!;
        public DbSet<Attraction> Attractions { get; set; } = null!;
        public DbSet<BarReviewAndRate> BarReviewsAndRates { get; set; } = null!;
        public DbSet<RestaurantReviewAndRate> RestaurantReviewsAndRates { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;

       // public DbSet<ApplicationUser> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CityEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BarEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RestaurantEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AttractionEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BarReviewAndRateConfiguration());
            modelBuilder.ApplyConfiguration(new RestaurantReviewAndRateConfiguration());
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasKey(ur => new { ur.UserId, ur.RoleId });

        }

       
    }
}