using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RaterTravel.Common.EntityValidationConstants.Employee;

namespace RatedTravel.Data.DataModels
{
    public class Employee
    {
        public Employee()
        {
            this.Id = Guid.NewGuid();
            this.OwnedCities = new HashSet<City>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(EmployeeNameMaxLength)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(EmployeePhoneMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        public Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;

        public virtual ICollection<City> OwnedCities { get; set; }
    }
}
