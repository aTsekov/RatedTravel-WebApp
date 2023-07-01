using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RaterTravel.Common.EntityValidationConstants.Restaurant;

namespace RatedTravel.Data.DataModels
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(RestaurantNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [MaxLength(RestaurantAddressMaxLength)]
        public string Address { get; set; } = null!;

        [Required]
        [MaxLength(RestaurantDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public int OverallScore { get; set; }

        public bool IsActive { get; set; }

        [Required]
        [ForeignKey("City")]
        public int CityId { get; set; }

        // Navigation property
        public virtual City City { get; set; } = null!;
        public virtual ICollection<RestaurantReviewAndRate> Reviews { get; set; } = new List<RestaurantReviewAndRate>();
    }
}
