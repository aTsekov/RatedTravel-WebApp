using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RatedTravel.Common.EntityValidationConstants.Attraction;

namespace RatedTravel.Data.DataModels
{
    public class Attraction
    {
        public Attraction()
        {
            
        }
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

        public Guid? EmployeeId { get; set; }

        public virtual Employee Employee { get; set; } = null!;

        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; } = null!;

        [ForeignKey("City")]
        public Guid CityId { get; set; }
       
        public virtual City City { get; set; } = null!;
    }
}
