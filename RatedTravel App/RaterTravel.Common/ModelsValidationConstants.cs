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

		public static class CityFormModel
		{
			public const int CityNameMaxLength = 50;
			public const int CountryNameMaxLength = 100;
			public const int CityDescriptionMaxLength = 3000;

			public const int CityNameMinLength = 2;
			public const int CountryNameMinLength = 2;
			public const int CityDescriptionMinLength = 10;
			
		}

        public static class RestaurantFormModel
        {
            public const int RestaurantNameMaxLength = 50;
            public const int RestaurantNameMinLength = 3;

            public const int RestaurantAddressMaxLength = 150;
            public const int RestaurantAddressMinLength = 10;

            public const int RestaurantDescriptionMaxLength = 3000;
            public const int RestaurantDescriptionMinLength = 10;

            public const int RestaurantOverallMaxLength = 1;
            public const int RestaurantOverallMinLength = 5;

        }
    }
}
