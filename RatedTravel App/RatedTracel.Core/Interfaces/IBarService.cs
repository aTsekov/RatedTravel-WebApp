using RatedTravel.Web.ViewModels.Bar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatedTravel.Core.Interfaces
{
    public interface IBarService
    {

        Task CreateBarAsync(string userId, BarFormModel formModel);

        Task<bool> DoesBarExistsByIdAsync(string barId);
        Task<bool> DoesBarExistsByName(string barName);

        Task<IEnumerable<BarAllModel>> AllBarsInACityAsync(string cityId);
        Task<BarDetailsView> DetailsAsync(string cityId, string barId);

        Task<double> GetOverallScoreOfBar(string barId);

        Task SendReviewAsync(string barId, BarRateAndReviewModel model);

        Task<BarFormModel> GetBarForEditAsync(string barId);

        Task<BarDeleteModel> GetBarForDeleteAsync(string barId);

        Task DeleteReviewByIdAsync(string reviewId);

        Task DeleteBarByIdAsync(string barId);

        Task EditBarByIdAndFormModelAsync(int barId, BarFormModel barFormModel);

        Task<IEnumerable<BarAllModel>> AllBarsAsync();


    }
}
