using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaterTravel.Common
{
	public static class ModelsValidationConstants
	{
		public static class EmployeeModel
		{
			public const int EmployeeNameMaxLength = 100;
			public const int EmployeeNameMinLength = 3;
			public const int EmployeePhoneMaxLength = 15;
			public const int EmployeePhoneMinLength = 10;
		}
	}
}
