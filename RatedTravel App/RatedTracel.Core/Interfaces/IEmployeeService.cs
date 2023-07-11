using RatedTravel.Web.ViewModels.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatedTravel.Core.Interfaces
{
    public interface IEmployeeService
    {
        Task<bool> EmployeeExistsByIdAndHasMoreThanThreeCreatedItemsAsync(string userId);
        Task<bool> EmployeeExistsByPhoneNumberAsync(string phoneNumber);

        Task CreateEmployeeAsync(string userId, BecomeEmployeeFormModel model);
    }
}
