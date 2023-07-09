using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatedTravel.Data.DataModels
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
            this.CreatedBars= new HashSet<Bar>();
            this.CreatedRestaurants= new HashSet<Restaurant>();
            this.CreatedAttractions= new HashSet<Attraction>();
        }

        public virtual ICollection<Bar> CreatedBars { get; set; }
        public virtual ICollection<Restaurant> CreatedRestaurants { get; set; }
        public virtual ICollection<Attraction> CreatedAttractions { get; set; }
       
    }
}
