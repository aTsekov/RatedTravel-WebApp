using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static RatedTravel.Common.ModelsValidationConstants.CityFormModel;
using static RatedTravel.Common.ModelsValidationConstants.RestaurantFormModel;

namespace RatedTravel.Web.ViewModels.Restaurant
{
    public class RestaurantFormModel
    {

        public int Id { get; set; }

        [Required]

        [StringLength(RestaurantNameMaxLength, MinimumLength = RestaurantNameMinLength)]
        public string Name { get; set; } = null!;

        [Required]

        [StringLength(CityNameMaxLength, MinimumLength = CityNameMinLength)]
        public string CityName { get; set; } = null!;

        [Required]
        [DisplayName("Upload Image")]
        public IFormFile ImageFile { get; set; } = null!;

        [Required]
        [StringLength(RestaurantAddressMaxLength, MinimumLength = RestaurantAddressMinLength)]
        public string Address { get; set; } = null!;

        [Required]
        [StringLength(RestaurantDescriptionMaxLength, MinimumLength = RestaurantDescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(RestaurantOverallMaxLength, RestaurantOverallMinLength)]
        public int OverallScore { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public string CityId { get; set; } = null!;

        


    }
}
