using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RaterTravel.Common.EntityValidationConstants.City;

namespace RatedTravel.Data.DataModels
{
    
    public class City
    {
        public City()
        {
            this.Id = new Guid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(CityNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(CountryNameMaxLength)]
        public string Country { get; set; } = null!;

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [MaxLength(CityDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public int NightlifeScore { get; set; }

        public int TransportScore { get; set; }

        public bool IsActive { get; set; }

        public Guid EmployeeId { get; set; }

        public virtual Employee Employee { get; set; } = null!;

        public Guid? UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public virtual ICollection<Bar> Bars { get; set; } = new List<Bar>();
        public virtual ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
        public virtual ICollection<Attraction> Attractions { get; set; } = new List<Attraction>();
    }
}
