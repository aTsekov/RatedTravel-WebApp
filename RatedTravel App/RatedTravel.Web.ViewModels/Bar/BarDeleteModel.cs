using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatedTravel.Web.ViewModels.Bar
{
    public class BarDeleteModel
    {
        public string Id { get; set; } = null!;


        public string Name { get; set; } = null!;

        public string City { get; set; } = null!;

        [Required]
        [DisplayName("Image")]
        public string ImageUrl { get; set; } = null!;

        public string? EmployeeId { get; set; }
    }
}
