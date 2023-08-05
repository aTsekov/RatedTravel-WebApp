using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static RatedTravel.Common.ModelsValidationConstants.CityFormModel;
using static RatedTravel.Common.ModelsValidationConstants.BarFormModel;

namespace RatedTravel.Web.ViewModels.Bar
{
	public class BarAllModel
	{


		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public string CityName { get; set; } = null!;

		
		public string Image { get; set; } = null!;

		public string Address { get; set; } = null!;

		public string Description { get; set; } = null!;
		public string Website { get; set; } = null!;

        public double OverallScore { get; set; }

		public string UserId { get; set; } = null!;

		public string CityId { get; set; } = null!;

	}
}
