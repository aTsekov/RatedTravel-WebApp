using Microsoft.EntityFrameworkCore;
using RatedTravel.Core.Interfaces;
using RatedTravel.Data;
using RatedTravel.Web.ViewModels.Bar;
using City = RatedTravel.Data.DataModels.City;
using Bar = RatedTravel.Data.DataModels.Bar;
using BarReviewAndRate = RatedTravel.Data.DataModels.BarReviewAndRate;

namespace RatedTravel.Core.Services
{

    public class BarService : IBarService
    {

        private readonly RatedTravelDbContext dbContext;
        private readonly ICityService cityService;
        private readonly IEmployeeService employeeService;

        public BarService(RatedTravelDbContext dbContext, ICityService cityService, IEmployeeService employeeService)
        {
            this.dbContext = dbContext;
            this.cityService = cityService;
            this.employeeService = employeeService;
        }


        public async Task CreateBarAsync(string userId, BarFormModel formModel)
        {

            var cityIdOfBar = await cityService.GetCityIdByName(formModel.CityName);

            if (cityIdOfBar == null)
            {
                throw new ArgumentException("Invalid city Id");
            }


            Bar newBar = new Bar
            {
                Name = formModel.Name,
                Address = formModel.Address,
                Description = formModel.Description,
                Website = formModel.Website,
                OverallScore = formModel.OverallScore,
                UserId = Guid.Parse(userId),
                CityId = Guid.Parse(cityIdOfBar),
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
                newBar.ImageUrl = fileName;
            }
            else
            {
                // If the image file is not uploaded, set the image URL to null
                newBar.ImageUrl = null;
            }

            await this.dbContext.Bars.AddAsync(newBar);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<bool> DoesBarExistsByIdAsync(string barId)
        {
            return await dbContext.Bars.Where(r => r.IsActive == true).AnyAsync(c => c.Id.ToString() == barId);
        }

        public async Task<bool> DoesBarExistsByName(string barName)
        {
            return await dbContext.Bars.Where(r => r.IsActive).AnyAsync(r => r.Name == barName);
        }

        public async Task<IEnumerable<BarAllModel>> AllBarsInACityAsync(string cityId)
        {
            List<BarAllModel> bars = await dbContext.Bars
                .Where(b => b.City.Id.ToString() == cityId && b.IsActive)
                .Select(b => new BarAllModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    CityName = b.City.Name,
                    Image = b.ImageUrl,
                    Description = b.Description,
                    Website = b.Website,
                    Address = b.Address,
                    UserId = b.UserId.ToString(),
                    CityId = b.City.Id.ToString()
                })
                .ToListAsync();

            foreach (var bar in bars)
            {
                double totalScore = await GetOverallScoreOfBar(bar.Id.ToString());
                bar.OverallScore = totalScore;
            }

            return bars;
        }

        public async Task<BarDetailsView> DetailsAsync(string cityId, string barId)
        {
            if (string.IsNullOrEmpty(cityId) || string.IsNullOrEmpty(barId))
            {
                throw new ArgumentException("CityId and BarId cannot be null or empty.");
            }

            var city = await dbContext.Cities
                .Where(c => c.Id.ToString() == cityId && c.IsActive)
                .FirstOrDefaultAsync();

            if (city == null)
            {
                throw new ArgumentException("City not found.");
            }

            var bar = await dbContext.Bars
                .Include(b => b.Reviews.Where(review => review.IsActive == true))
                .Where(b => b.City.Id.ToString() == cityId && b.Id.ToString() == barId && b.IsActive == true)
                .FirstOrDefaultAsync();

            if (bar == null)
            {
                throw new ArgumentException("Bar not found.");
            }

            double totalScore = await GetOverallScoreOfBar(bar.Id.ToString());

            var model = new BarDetailsView
            {
                Id = bar.Id,
                Name = bar.Name,
                CityName = city.Name,
                Image = bar.ImageUrl,
                Address = bar.Address,
                Description = bar.Description,
                Website = bar.Website,
                OverallScore = totalScore,
                UserId = bar.UserId.ToString(),
                CityId = bar.City.Id.ToString(),
                Reviews = bar.Reviews.Select(r => new BarReviewModel
                {
                    ReviewId = r.Id,
                    ReviewText = r.ReviewText,
                    LocationRate = r.LocationRate,
                    MusicRate = r.MusicRate,
                    PriceRate = r.PriceRate,
                    ServiceRate = r.ServiceRate
                })
            };

            return model;
        }




        public async Task<double> GetOverallScoreOfBar(string barId)
        {
            List<int> musicRates = await dbContext.BarReviewsAndRates
                .Where(s => s.IsActive && s.BarId.ToString() == barId)
                .Select(s => s.MusicRate)
                .ToListAsync();



            List<int> locationRates = await dbContext.BarReviewsAndRates
                .Where(s => s.IsActive && s.BarId.ToString() == barId)
                .Select(s => s.LocationRate)
                .ToListAsync();

            List<int> priceRates = await dbContext.BarReviewsAndRates
                .Where(s => s.IsActive && s.BarId.ToString() == barId)
                .Select(s => s.PriceRate)
                .ToListAsync();

            List<int> serviceRates = await dbContext.BarReviewsAndRates
                .Where(s => s.IsActive && s.BarId.ToString() == barId)
                .Select(s => s.ServiceRate)
                .ToListAsync();

            // Check if any rates are available
            if (musicRates.Count == 0 || locationRates.Count == 0 || priceRates.Count == 0 || serviceRates.Count == 0)
            {

                return 0;
            }

            double totalScore = (musicRates.Average() + locationRates.Average() + priceRates.Average() +
                                 serviceRates.Average()) / 4.0; // Convert to double by adding ".0"
            double roundedScore = Math.Round(totalScore, 1);

            return roundedScore;
        }

        public async Task SendReviewAsync(string barId, BarRateAndReviewModel model)
        {
            var bar = await dbContext.Bars
                .FirstOrDefaultAsync(r => r.Id.ToString() == barId);

            if (bar == null)
            {
                throw new ArgumentException("Bar not found.");
            }

            BarReviewAndRate review = new BarReviewAndRate
            {
                ReviewText = model.ReviewText,
                MusicRate = model.MusicRate,
                LocationRate = model.LocationRate,
                PriceRate = model.PriceRate,
                ServiceRate = model.ServiceRate,
                BarId = bar.Id
            };

            await dbContext.BarReviewsAndRates.AddAsync(review);
            await dbContext.SaveChangesAsync();
        }




        public async Task<BarFormModel> GetBarForEditAsync(string barId)
        {
            Bar? barToEdit = await this.dbContext.Bars
                .Where(b => b.IsActive == true && b.Id == int.Parse(barId))
                .FirstOrDefaultAsync();

            if (barToEdit == null)
            {
                // Handle the case where the bar with the provided ID does not exist.
                throw new ArgumentException("Invalid bar ID.");
            }

            City? city = await this.dbContext.Cities
                .Where(c => c.IsActive == true && c.Id == barToEdit.CityId)
                .FirstOrDefaultAsync();

            if (city == null)
            {
                throw new ArgumentException("Invalid city ID.");
            }

            return new BarFormModel
            {
                Id = barToEdit.Id,
                Name = barToEdit.Name,
                CityName = city.Name,
                Address = barToEdit.Address,
                Description = barToEdit.Description,
                Website = barToEdit.Website,
                OverallScore = barToEdit.OverallScore,

            };
        }



        public async Task EditBarByIdAndFormModelAsync(int barId, BarFormModel barFormModel)
        {
            Bar? bar = await this.dbContext.Bars
                .Where(r => r.IsActive == true && r.Id == barId)
                .FirstOrDefaultAsync();

            City? city = await this.dbContext.Cities
                .Where(c => c.IsActive == true && c.Id.ToString() == barFormModel.CityId)
                .FirstOrDefaultAsync();

            if (city == null)
            {
                throw new ArgumentException("Invalid city ID.");
            }

            if (bar == null)
            {
                throw new ArgumentException("Invalid bar ID.");
            }

            bar.Name = barFormModel.Name;
            bar.City.Name = city.Name;
            bar.Address = barFormModel.Address;
            bar.Description = barFormModel.Description;
            bar.Website = barFormModel.Website;
            bar.OverallScore = barFormModel.OverallScore;

            // Handle the uploaded image file
            if (barFormModel.ImageFile != null && barFormModel.ImageFile.Length > 0)
            {
                // Generate the filename for the image using the bar name
                var extension = Path.GetExtension(barFormModel.ImageFile.FileName);
                var fileName = barFormModel.Name + extension;
                var filePath = Path.Combine("wwwroot", "images", fileName);

                // Save the image file to the specified path
                await using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await barFormModel.ImageFile.CopyToAsync(fileStream);
                }

                // Update the bar entity with the image path
                bar.ImageUrl = fileName;
            }

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<BarAllModel>> AllBarsAsync()
        {
            List<BarAllModel> bars = await dbContext.Bars
                .Where(b => b.IsActive)
                .Select(b => new BarAllModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    CityName = b.City.Name,
                    Image = b.ImageUrl,
                    Description = b.Description,
                    Website = b.Website,
                    Address = b.Address,
                    UserId = b.UserId.ToString(),
                    CityId = b.City.Id.ToString()
                })
                .ToListAsync();

            foreach (var bar in bars)
            {
                double totalScore = await GetOverallScoreOfBar(bar.Id.ToString());
                bar.OverallScore = totalScore;
            }

            return bars;
        }


        public async Task<BarDeleteModel> GetBarForDeleteAsync(string barId)
        {
            Bar? barToDelete = await dbContext.Bars
                .Include(b => b.City)
                .Where(b => b.IsActive)
                .FirstOrDefaultAsync(r => r.Id.ToString() == barId);

            return new BarDeleteModel
            {
                Id = barToDelete.Id.ToString(),
                Name = barToDelete.Name,
                City = barToDelete.City.Name,
                ImageUrl = barToDelete.ImageUrl
            };
        }

        public async Task DeleteReviewByIdAsync(string reviewId)
        {
            var reviewToDelete = await dbContext.BarReviewsAndRates.Where(b => b.IsActive)
                .FirstOrDefaultAsync(b => b.Id.ToString() == reviewId);

            if (reviewToDelete == null)
            {
                throw new ArgumentException($"Review with ID '{reviewId}' not found.");
            }

            reviewToDelete.IsActive = false;

            await dbContext.SaveChangesAsync();
        }


        public async Task DeleteBarByIdAsync(string barId)
        {
            try
            {
                Bar? barToDelete = await dbContext.Bars
                .Where(r => r.IsActive)
                .FirstOrDefaultAsync(r => r.Id.ToString() == barId);

                barToDelete.IsActive = false;
                await dbContext.SaveChangesAsync();
            }
            catch (ArgumentException)
            {

                throw new ArgumentException("Bar not found");
            }
            
        }
    }


}
