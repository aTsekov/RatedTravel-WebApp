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
            
            this.OwnedCities = new HashSet<City>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(EmployeeNameMaxLength)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(EmployeePhoneMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        public int UserId { get; set; }

        public virtual AppUser AppUser { get; set; } = null!;

        public virtual ICollection<City> OwnedCities { get; set; }
    }
}
