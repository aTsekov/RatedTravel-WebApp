﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RatedTravel.Core.Interfaces;
using RatedTravel.Data;
using RatedTravel.Data.DataModels;
using RatedTravel.Web.ViewModels.City;
using RatedTravel.Web.ViewModels.Home;

namespace RatedTravel.Core.Services
{
	public class CityService : ICityService
	{
		private readonly RatedTravelDbContext dbContext;

		public CityService(RatedTravelDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<IEnumerable<IndexViewModel>> OurCitiesAsync()
		{
			IEnumerable<IndexViewModel> ourCities = await this.dbContext.Cities.OrderBy(c => c.Id)
				.Select(c => new IndexViewModel()
				{
					Id = c.Id.ToString(),
					Name = c.Name,
					ImageUrl = c.ImageUrl
				}).ToListAsync();

			return ourCities;
		}

		public async Task<bool> DoesCityExistsAsync(string city)
		{
			return await dbContext.Cities.AnyAsync(c => c.Name == city);
		}




        public async Task CreateCityAsync(string emplId, string userId, CityFormModel formModel)
        {
            City newCity = new City()
            {
                Name = formModel.Name,
                Country = formModel.Country,
                Description = formModel.Description,
                NightlifeScore = formModel.NightlifeScore,
                TransportScore = formModel.TransportScore,
                EmployeeId = Guid.Parse(emplId),
                UserId = Guid.Parse(userId)
            };

            // Handle the uploaded image file
            if (formModel.ImageFile != null && formModel.ImageFile.Length > 0)
            {
                // Generate the filename for the image using the city name
                var extension = Path.GetExtension(formModel.ImageFile.FileName);
                var fileName = formModel.Name + extension;
                var filePath = Path.Combine("wwwroot", "images", fileName);

                // Save the image file to the specified path
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formModel.ImageFile.CopyToAsync(fileStream);
                }

                // Update the city entity with the image path
                newCity.ImageUrl = fileName;
            }
            else
            {
                // If the image file is not uploaded, set the image URL to null
                newCity.ImageUrl = null;
            }

            await this.dbContext.Cities.AddAsync(newCity);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<CitySelectModel> SelectCityAsync(string cityId)
        {
            City? currentCity = await this.dbContext.Cities.FirstOrDefaultAsync(c => c.Id.ToString() == cityId);

            return new CitySelectModel
            {
                Id = currentCity.Id.ToString(),
                Name = currentCity.Name,
                Country = currentCity.Country,
                Description = currentCity.Description,
                ImageUrl = currentCity.ImageUrl,
                NightlifeScore = currentCity.NightlifeScore,
                TransportScore = currentCity.TransportScore,
                EmployeeId = currentCity.EmployeeId.ToString(),
            };


        }
    }
}
    

