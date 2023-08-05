using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RatedTravel.Common.EntityValidationConstants.User;

namespace RatedTravel.Data.DataModels
{
    [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
            this.CreatedBars= new HashSet<Bar>();
            this.CreatedRestaurants= new HashSet<Restaurant>();
            this.CreatedAttractions= new HashSet<Attraction>();
        }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(LastNameMaxLength)]
		public string LastName { get; set; } = null!;

        public virtual ICollection<Bar> CreatedBars { get; set; }
        public virtual ICollection<Restaurant> CreatedRestaurants { get; set; }
        public virtual ICollection<Attraction> CreatedAttractions { get; set; }
       
    }
}
