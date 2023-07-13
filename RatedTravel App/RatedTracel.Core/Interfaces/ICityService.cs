using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RatedTravel.Web.ViewModels.City;
using RatedTravel.Web.ViewModels.Home;

namespace RatedTravel.Core.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<IndexViewModel>> OurCitiesAsync();
        Task<bool> DoesCityExistsAsync(string city);

        Task CreateCityAsync(string emplId, string userId, CityFormModel formModel);

        Task SelectCityAsync(CitySelectModel model);
    }
}
