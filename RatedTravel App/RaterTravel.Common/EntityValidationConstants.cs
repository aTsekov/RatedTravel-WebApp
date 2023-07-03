using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaterTravel.Common
{
    public static class EntityValidationConstants
    {
        public static class City
        {
            public const int CityNameMaxLength = 50;
            public const int CountryNameMaxLength = 100;
            public const int CityDescriptionMaxLength = 3000;
            public const int ImageUrlMaxLength = 2048;
        }

        public static class Bar
        {
            public const int BarNameMaxLength = 50;
            public const int BarAddressMaxLength = 100;
            public const int BarDescriptionMaxLength = 1000;
            public const int ImageUrlMaxLength = 2048;
            public const int WebSiteMaxLength = 100;
        }
        public static class Restaurant
        {
            public const int RestaurantNameMaxLength = 50;
            public const int RestaurantAddressMaxLength = 100;
            public const int RestaurantDescriptionMaxLength = 1000;
            public const int ImageUrlMaxLength = 2048;
            public const int RestaurantSiteMaxLength = 100;
        }
        public static class Attraction
        {
            public const int AttractionNameMaxLength = 50;
            public const int AttractionAddressMaxLength = 100;
            public const int AttractionDescriptionMaxLength = 1000;
            public const int ImageUrlMaxLength = 2048;            
        }
        public static class BarReviewAndRate
        {
            public const int ReviewTextMaxLength = 1000;
        }
        public static class RestaurantReviewAndRate
        {
            public const int ReviewTextMaxLength = 1000;
        }
        public static class Employee
        {
            public const int EmployeeNameMaxLength = 100;
            public const int EmployeePhoneMaxLength = 15;
        }
    }
}
