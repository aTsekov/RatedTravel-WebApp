
using System.ComponentModel.DataAnnotations;
using static RatedTravel.Common.ModelsValidationConstants.RestaurantReviewModel;

namespace RatedTravel.Web.ViewModels.Restaurant
{
    public class RestaurantRateAndReviewModel
    {
        public string ReviewId { get; set; } = null!;

        public string RestaurantId { get; set; } = null!;

        [Required]
        [StringLength(RestaurantReviewTextMaxLength, MinimumLength = RestaurantReviewTextMinLength)]
        public string ReviewText { get; set; } = null!;

        [Required]
        [Range(RestaurantLocationRateMaxLength, RestaurantLocationRateMinLength)]
        public int LocationRate { get; set; }
        [Range(RestaurantFoodRateMaxLength, RestaurantFoodRateMinLength)]
        public int FoodRate { get; set; }
        [Range(RestaurantPriceRateMaxLength, RestaurantPriceRateMinLength)]
        public int PriceRate { get; set; }
        [Range(RestaurantServiceRateMaxLength, RestaurantServiceRateMinLength)]
        public int ServiceRate { get; set; }
    }
}
