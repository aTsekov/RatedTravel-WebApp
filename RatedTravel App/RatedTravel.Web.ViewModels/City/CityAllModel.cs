using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RatedTravel.Web.ViewModels.City
{
    public class CityAllModel
    {
        public string Id { get; set; } = null!;

      
        public string Name { get; set; } = null!;

     
        public string Country { get; set; } = null!;

        
        [DisplayName("Upload")]
        public string ImageUrl { get; set; } = null!;

        public string Description { get; set; } = null!;

        [Display(Name = "Nightlife Score")]

        public int NightlifeScore { get; set; }

        [Display(Name = "Transportation Score")]
        public int TransportScore { get; set; }

        public string? EmployeeId { get; set; }
    }
}
