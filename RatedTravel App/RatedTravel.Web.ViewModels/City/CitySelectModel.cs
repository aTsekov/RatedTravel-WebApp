﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using static RatedTravel.Common.GeneralApplicationConstants;
using static RatedTravel.Common.ModelsValidationConstants.CityFormModel;

namespace RatedTravel.Web.ViewModels.City
{
    public class CitySelectModel
    {


        public string Id { get; set; } = null!;

        [Required]
        [StringLength(CityNameMaxLength, MinimumLength = CityNameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(CountryNameMaxLength, MinimumLength = CountryNameMinLength)]
        public string Country { get; set; } = null!;

        [Required]
        [DisplayName("Upload Image")]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [StringLength(CityDescriptionMaxLength, MinimumLength = CityDescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Range(MinScore, MaxScore)]
        [Display(Name = "Nightlife Score")]

        public int NightlifeScore { get; set; }

        [Range(MinScore, MaxScore)]
        [Display(Name = "Transportation Score")]
        public int TransportScore { get; set; }

        public string? EmployeeId { get; set; }

    }
}
