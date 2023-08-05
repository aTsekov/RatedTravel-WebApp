
using System.ComponentModel.DataAnnotations;
using static RatedTravel.Common.ModelsValidationConstants.BarReviewModel;

namespace RatedTravel.Web.ViewModels.Bar
{
    public class BarRateAndReviewModel
    {
        public string ReviewId { get; set; } = null!;

        public string BarId { get; set; } = null!;

        [Required]
        [StringLength(BarReviewTextMaxLength, MinimumLength = BarReviewTextMinLength)]
        public string ReviewText { get; set; } = null!;

        [Required]
        [Range(BarLocationRateMaxLength, BarLocationRateMinLength)]
        public int LocationRate { get; set; }
        [Range(BarMusicRateMaxLength, BarMusicRateMinLength)]
        public int MusicRate { get; set; }

        [Range(BarPriceRateMaxLength, BarPriceRateMinLength)]
        public int PriceRate { get; set; }
        [Range(BarServiceRateMaxLength, BarServiceRateMinLength)]
        public int ServiceRate { get; set; }
    }
}
