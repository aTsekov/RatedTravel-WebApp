using RatedTravel.Web.ViewModels.Restaurant
    ;

namespace RatedTravel.Core.Interfaces
{
    public interface IRestaurantService
    {

        
        Task CreateRestaurantAsync(string userId, RestaurantFormModel formModel);

        Task<bool> DoesRestaurantExistsByIdAsync(string restaurantId);
        Task<bool> DoesRestaurantExistsByName(string restaurantName);

        Task<IEnumerable<RestaurantAllModel>> AllRestaurantsInACityAsync(string cityId);
        Task<RestaurantDetailsView> DetailsAsync(string cityId, string restaurantId);

        Task<double> GetOverallScoreOfRestaurant(string restaurantId);

    }
}
