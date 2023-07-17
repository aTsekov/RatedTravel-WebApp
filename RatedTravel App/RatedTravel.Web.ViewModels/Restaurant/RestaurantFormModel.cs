using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RaterTravel.Common.ModelsValidationConstants.RestaurantFormModel;
using static RaterTravel.Common.ModelsValidationConstants.CityFormModel;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace RatedTravel.Web.ViewModels.Restaurant
{
    public class RestaurantFormModel
    {

        [Key]
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
        [Range(RestaurantOverallMinLength,RestaurantNameMaxLength)]
        public int OverallScore { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public string CityId { get; set; } = null!;

        


    }
}
