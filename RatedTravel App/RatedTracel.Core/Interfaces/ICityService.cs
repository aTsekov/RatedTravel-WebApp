using RatedTravel.Web.ViewModels.City;
using RatedTravel.Web.ViewModels.Home;

namespace RatedTravel.Core.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<IndexViewModel>> OurCitiesAsync();
        Task<IEnumerable<CityAllModel>> AllCitiesAsync();
        Task<bool> DoesCityExistsAsync(string city);

        Task CreateCityAsync(string emplId, string userId, CityFormModel formModel);

        Task<CitySelectModel> SelectCityAsync(string cityId);

        Task<CityDeleteModel> GetCityForDelete(string cityId);

        Task<CityFormModel> EditAsync(string cityId);
         Task<bool> DoesCityExistsByIdAsync(string cityId);

         Task EditCityByIdAndFormModelAsync(string houseId, CityFormModel cityFormModel);

         Task DeleteCityByIdAsync(string cityId);
    }
}
