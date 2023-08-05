using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RatedTravel.Common.ModelsValidationConstants.RestaurantDetailsModel;

namespace RatedTravel.Web.ViewModels.Restaurant
{
	public class RestaurantReviewModel
    {

		public int ReviewId { get; set; }

		[Required]
		[StringLength(RestaurantReviewTextMaxLength, MinimumLength = RestaurantReviewTextMinLength)]
		public string ReviewText { get; set; } = null!;

		[StringLength(RestaurantLocationRateMaxLength, MinimumLength = RestaurantLocationRateMinLength)]
		public int LocationRate { get; set; }
		[StringLength(RestaurantFoodRateMaxLength, MinimumLength = RestaurantFoodRateMinLength)]
		public int FoodRate { get; set; }
		[StringLength(RestaurantPriceRateMaxLength, MinimumLength = RestaurantPriceRateMinLength)]
		public int PriceRate { get; set; }
		[StringLength(RestaurantServiceRateMaxLength, MinimumLength = RestaurantServiceRateMinLength)]
		public int ServiceRate { get; set; }

		
	}
}
