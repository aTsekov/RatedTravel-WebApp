
using System.ComponentModel.DataAnnotations;

using static RaterTravel.Common.ModelsValidationConstants.EmployeeModel;


namespace RatedTravel.Web.ViewModels.Employee
{
    public class BecomeEmployeeFormModel
    {

	    [Required]
        [StringLength(EmployeeNameMaxLength, MinimumLength = EmployeeNameMinLength)]
	    [Display(Name = "Full Name")]
		public string FullName { get; set; } = null!;

        [Required]
        [StringLength(EmployeePhoneMaxLength, MinimumLength = EmployeePhoneMinLength)]
        [Phone]
        [Display (Name = "Phone")]
        public string PhoneNumber { get; set; } = null!;

    }
}
