using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RatedTravel.Common.EntityValidationConstants.Bar;

namespace RatedTravel.Data.DataModels
{
    public class Bar
    {
        public Bar()
        {
            
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(BarNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(BarAddressMaxLength)]
        public string Address { get; set; } = null!;

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [MaxLength(BarDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public int OverallScore { get; set; }
        [MaxLength(WebSiteMaxLength)]
        public string Website { get; set; } = null!;

        public bool IsActive { get; set; }

        public Guid? EmployeeId { get; set; }

        public virtual Employee Employee { get; set; } = null!;

        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; } = null!;

        [ForeignKey("City")]
        public Guid CityId { get; set; }

        public virtual City City { get; set; } = null!;
        public virtual ICollection<BarReviewAndRate> Reviews { get; set; } = new List<BarReviewAndRate>();
    }
}
