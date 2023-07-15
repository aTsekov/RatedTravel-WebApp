using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using Microsoft.AspNetCore.Http;
using static RaterTravel.Common.ModelsValidationConstants.CityFormModel;
using static RaterTravel.Common.GeneralApplicationConstants;

namespace RatedTravel.Web.ViewModels.City
{
	public class CityFormModel
	{
        public string? Id { get; set; } 

        [Required]
		[StringLength(CityNameMaxLength, MinimumLength = CityNameMinLength)]
		public string Name { get; set; } = null!;

		[Required]
		[StringLength(CountryNameMaxLength, MinimumLength = CountryNameMinLength)]
		public string Country { get; set; } = null!;

		[Required]
		[DisplayName("Upload Image")]
		public IFormFile ImageFile { get; set; } = null!;

		[Required]
		[StringLength(CityDescriptionMaxLength, MinimumLength = CityDescriptionMinLength)]
		public string Description { get; set; } = null!;

		[Range(MinScore,MaxScore)]

		[Display(Name = "Nightlife Score")]

		public int NightlifeScore { get; set; }

		[Range(MinScore, MaxScore)]
		[Display(Name = "Transportation Score")]
		public int TransportScore { get; set; }

		public string? EmployeeId { get; set; }



	}
}

