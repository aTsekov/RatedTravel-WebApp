﻿
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
            IEnumerable<IndexViewModel> ourCities = await this.dbContext.Cities.Where(c => c.IsActive == true).OrderBy(c => c.Id)
                .Select(c => new IndexViewModel()
                {
                    Id = c.Id.ToString(),
                    Name = c.Name,
                    ImageUrl = c.ImageUrl
                }).ToListAsync();

            return ourCities;
        }

        public async Task<IEnumerable<CityAllModel>> AllCitiesAsync()
        {

            List<CityAllModel> cities = await dbContext.Cities
                .Where(c => c.IsActive == true)
                .Select(c => new CityAllModel
                {
                    Id = c.Id.ToString(),
                    Name = c.Name,
                    Country = c.Country,
                    ImageUrl = c.ImageUrl,
                    Description = c.Description,
                    NightlifeScore = c.NightlifeScore,
                    TransportScore = c.TransportScore,
                    EmployeeId = c.EmployeeId.ToString()
                })
                .ToListAsync();

            return cities;

        }

        public async Task<bool> DoesCityExistsAsync(string city)
		{
			return await dbContext.Cities.Where(c => c.IsActive == true).AnyAsync(c => c.Name == city);
		}

        public async Task<bool> DoesCityExistsByIdAsync(string cityId)
        {
            return await dbContext.Cities.Where(c => c.IsActive == true).AnyAsync(c => c.Id.ToString() == cityId);
        }

        public async Task EditCityByIdAndFormModelAsync(string cityId, CityFormModel cityFormModel)
        {
            City city = await this.dbContext.Cities.Where(c => c.IsActive).FirstAsync(c => c.Id.ToString() == cityId);

            city.Name = cityFormModel.Name;
            city.Country = cityFormModel.Country;
            city.Description = cityFormModel.Description;
            city.NightlifeScore = cityFormModel.NightlifeScore;
            city.TransportScore = cityFormModel.TransportScore;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteCityByIdAsync(string cityId)
        {
            City cityToDelete = await this.dbContext.Cities.Where(c => c.IsActive)
                .FirstAsync(c => c.Id.ToString() == cityId);

            cityToDelete.IsActive = false;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<string> GetCityIdByName(string cityName)
        {
            try
            {
                var city = await dbContext.Cities.Where(c => c.IsActive).FirstAsync(c => c.Name == cityName);

                string result = city.Id.ToString();

                return result;
            }
            catch (ArgumentException)
            {

                throw new ArgumentException("Invalid city name.");
            }
            
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
                await using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formModel.ImageFile.CopyToAsync(fileStream);
                }

                // Update the city entity with the image path
                newCity.ImageUrl = fileName;
            }
            else
            {
                // If the image file is not uploaded, set the image URL to null
                newCity.ImageUrl = "Unknown.jpg";
            }

            await this.dbContext.Cities.AddAsync(newCity);
            await this.dbContext.SaveChangesAsync();
        }

		public async Task<CitySelectModel> SelectCityAsync(string cityId)
		{
			try
			{
				City? currentCity = await this.dbContext.Cities
					.Where(c => c.IsActive == true)
					.FirstOrDefaultAsync(c => c.Id.ToString() == cityId);

				if (currentCity == null)
				{
					throw new Exception("City not found with the specified ID.");
				}

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
			catch (Exception ex)
			{

				throw new Exception("City not found with the specified ID.");
			}
		}


		public async Task<CityDeleteModel> GetCityForDelete(string cityId)
        {
            City cityToDelete = await this.dbContext.Cities.Where(c => c.IsActive == true)
                .FirstAsync(c => c.Id.ToString() == cityId);
            return new CityDeleteModel
            {
                Name = cityToDelete.Name,
                Country = cityToDelete.Country,
                ImageUrl = cityToDelete.ImageUrl
            };
        }

        public async Task<CityFormModel> EditAsync(string cityId)
        {
            City? cityToEdit = await this.dbContext.Cities.Where(c => c.IsActive == true).FirstOrDefaultAsync(c => c.Id.ToString() == cityId);

            return new CityFormModel
            {

                Name = cityToEdit.Name,
                Country = cityToEdit.Country,
                Description = cityToEdit.Description,
                NightlifeScore = cityToEdit.NightlifeScore,
                TransportScore = cityToEdit.TransportScore,
                
            };
        }


        

        
    }
}
    

