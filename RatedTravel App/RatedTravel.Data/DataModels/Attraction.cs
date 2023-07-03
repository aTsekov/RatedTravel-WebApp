using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RaterTravel.Common.EntityValidationConstants.Attraction;

namespace RatedTravel.Data.DataModels
{
    public class Attraction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(AttractionNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(AttractionDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        public int WorthVisitingScore { get; set; }

        public bool IsActive { get; set; }

        public int? EmployeeId { get; set; }

        public virtual Employee Employee { get; set; } = null!;

        public int AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; } = null!;

        [ForeignKey("City")]
        public int CityId { get; set; }
       
        public virtual City City { get; set; } = null!;
    }
}
