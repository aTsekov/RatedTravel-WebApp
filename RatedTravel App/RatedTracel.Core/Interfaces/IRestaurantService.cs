using RatedTravel.Web.ViewModels.Restaurant
    ;

namespace RatedTravel.Core.Interfaces
{
    public interface IRestaurantService
    {

        
        Task CreateRestaurantAsync(string userId, RestaurantFormModel formModel);

        Task<bool> DoesRestaurantExistsByIdAsync(string restaurantId);
        Task<bool> DoesRestaurantExistsByName(string restaurantName);

    }
}
