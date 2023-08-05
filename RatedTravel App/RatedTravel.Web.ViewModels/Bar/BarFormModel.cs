using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static RatedTravel.Common.ModelsValidationConstants.BarFormModel;
using static RatedTravel.Common.ModelsValidationConstants.CityFormModel;

namespace RatedTravel.Web.ViewModels.Bar
{
    public class BarFormModel
    {

        public int Id { get; set; }

        [Required]

        [StringLength(BarNameMaxLength, MinimumLength = BarNameMinLength)]
        public string Name { get; set; } = null!;

        [Required]

        [StringLength(CityNameMaxLength, MinimumLength = CityNameMinLength)]
        public string CityName { get; set; } = null!;

        [Required]
        [DisplayName("Upload Image")]
        public IFormFile ImageFile { get; set; } = null!;

        [Required]
        [StringLength(BarAddressMaxLength, MinimumLength = BarAddressMinLength)]
        public string Address { get; set; } = null!;

        [Required]
        [StringLength(BarDescriptionMaxLength, MinimumLength = BarDescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [StringLength(BarWebSiteMaxLength, MinimumLength = BarWebSiteMinLength)]
        public string Website { get; set; } = null!;


        [Required]
        [Range(BarOverallMaxLength, BarOverallMinLength)]
        public int OverallScore { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public string CityId { get; set; } = null!;

        


    }
}
