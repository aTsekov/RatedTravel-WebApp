using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RatedTravel.Common.ModelsValidationConstants.BarDetailsModel;

namespace RatedTravel.Web.ViewModels.Bar
{
	public class BarReviewModel
	{

		public int ReviewId { get; set; }

		[Required]
		[StringLength(BarReviewTextMaxLength, MinimumLength = BarReviewTextMinLength)]
		public string ReviewText { get; set; } = null!;

		[StringLength(BarLocationRateMaxLength, MinimumLength = BarLocationRateMinLength)]
		public int LocationRate { get; set; }

		[StringLength(BarMusicRateMaxLength, MinimumLength = BarMusicRateMinLength)]
		public int MusicRate { get; set; }

		[StringLength(BarPriceRateMaxLength, MinimumLength = BarPriceRateMinLength)]
		public int PriceRate { get; set; }

		[StringLength(BarServiceRateMaxLength, MinimumLength = BarServiceRateMinLength)]
		public int ServiceRate { get; set; }

		
	}
}
