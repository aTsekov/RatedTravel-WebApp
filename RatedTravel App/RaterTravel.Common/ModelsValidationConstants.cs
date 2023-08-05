using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatedTravel.Common
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

        public static class RestaurantDetailsModel
        {

	        public const int RestaurantReviewTextMinLength = 10;
	        public const int RestaurantReviewTextMaxLength = 1000;

	        public const int RestaurantLocationRateMaxLength = 1;
	        public const int RestaurantLocationRateMinLength = 5;

	        public const int RestaurantFoodRateMaxLength = 1;
	        public const int RestaurantFoodRateMinLength = 5;

	        public const int RestaurantPriceRateMaxLength = 1;
	        public const int RestaurantPriceRateMinLength = 5;

	        public const int RestaurantServiceRateMaxLength = 1;
	        public const int RestaurantServiceRateMinLength = 5;

		}

        public static class RestaurantReviewModel
        {
            public const int RestaurantReviewTextMinLength = 10;
            public const int RestaurantReviewTextMaxLength = 1000;

            public const int RestaurantLocationRateMaxLength = 1;
            public const int RestaurantLocationRateMinLength = 5;

            public const int RestaurantFoodRateMaxLength = 1;
            public const int RestaurantFoodRateMinLength = 5;

            public const int RestaurantPriceRateMaxLength = 1;
            public const int RestaurantPriceRateMinLength = 5;

            public const int RestaurantServiceRateMaxLength = 1;
            public const int RestaurantServiceRateMinLength = 5;

        }
        //---------

        public static class BarFormModel
        {
            public const int BarNameMaxLength = 50;
            public const int BarNameMinLength = 3;

            public const int BarAddressMaxLength = 150;
            public const int BarAddressMinLength = 10;


            public const int BarDescriptionMaxLength = 3000;
            public const int BarDescriptionMinLength = 10;

            public const int BarWebSiteMaxLength = 150;
            public const int BarWebSiteMinLength = 10;

            public const int BarOverallMaxLength = 1;
            public const int BarOverallMinLength = 5;

        }

        public static class BarDetailsModel
        {

            public const int BarReviewTextMinLength = 10;
            public const int BarReviewTextMaxLength = 1000;

            public const int BarLocationRateMaxLength = 1;
            public const int BarLocationRateMinLength = 5;

            public const int BarMusicRateMaxLength = 1;
            public const int BarMusicRateMinLength = 5;

            public const int BarPriceRateMaxLength = 1;
            public const int BarPriceRateMinLength = 5;

            public const int BarServiceRateMaxLength = 1;
            public const int BarServiceRateMinLength = 5;

        }

        public static class BarReviewModel
        {
            public const int BarReviewTextMinLength = 10;
            public const int BarReviewTextMaxLength = 1000;

            public const int BarLocationRateMaxLength = 1;
            public const int BarLocationRateMinLength = 5;

            public const int BarMusicRateMaxLength = 1;
            public const int BarMusicRateMinLength = 5;

            public const int BarPriceRateMaxLength = 1;
            public const int BarPriceRateMinLength = 5;

            public const int BarServiceRateMaxLength = 1;
            public const int BarServiceRateMinLength = 5;

        }

    }
}
