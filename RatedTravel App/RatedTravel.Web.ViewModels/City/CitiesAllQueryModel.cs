using System.ComponentModel.DataAnnotations;
using static RatedTravel.Common.GeneralApplicationConstants;

namespace RatedTravel.Web.ViewModels.City
{


    public class CitiesAllQueryModel
        {
            public CitiesAllQueryModel()
            {
                this.CurrentPage = DefaultPage;
                this.CitiesPerPage = EntitiesPerPage;
                this.Cities = new HashSet<CityAllModel>();
            }


            [Display(Name = "Search by City")]
            public string? SearchString { get; set; }

            public int CurrentPage { get; set; }

            [Display(Name = "Show Cities On Page")]
            public int CitiesPerPage { get; set; }

            public int TotalCities { get; set; }

            public IEnumerable<CityAllModel> Cities { get; set; }

            public int TotalPages => (int)Math.Ceiling((double)TotalCities / CitiesPerPage);
        }

    
}
