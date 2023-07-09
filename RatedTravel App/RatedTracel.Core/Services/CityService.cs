using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RatedTravel.Core.Interfaces;
using RatedTravel.Data;
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
    }
}
