using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RatedTravel.Common.EntityValidationConstants.Restaurant;

namespace RatedTravel.Data.DataModels
{
    public class Restaurant
    {
        public Restaurant()
        {
            
        }
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

        public Guid? EmployeeId { get; set; }

        public virtual Employee Employee { get; set; } = null!;

        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; } = null!;

        [ForeignKey("City")]
        public Guid CityId { get; set; }

        public virtual City City { get; set; } = null!;
        public virtual ICollection<RestaurantReviewAndRate> Reviews { get; set; } = new List<RestaurantReviewAndRate>();
    }
}
