﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RatedTravel.Common.ModelsValidationConstants.RestaurantDetailsModel;

namespace RatedTravel.Web.ViewModels.Restaurant
{
	public class RestaurantDetailsView
	{
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public string CityName { get; set; } = null!;

		public string Image { get; set; } = null!;

		public string Address { get; set; } = null!;

		public string Description { get; set; } = null!;

		public double OverallScore { get; set; }

		public string UserId { get; set; } = null!;

		public string EmployeeId { get; set; } = null!;

		public string CityId { get; set; } = null!;

		public IEnumerable<RestaurantReviewModel> Reviews { get; set; } = null!;





	}
}
