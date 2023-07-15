using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RatedTravel.Web.ViewModels.City
{
    public class CityDeleteModel
    {
        public string Id { get; set; } = null!;

        
        public string Name { get; set; } = null!;

        public string Country { get; set; } = null!;

        [Required]
        [DisplayName("Image")]
        public string ImageUrl { get; set; } = null!;

        public string? EmployeeId { get; set; }
    }
}
