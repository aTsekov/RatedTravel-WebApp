using RatedTravel.Web.ViewModels.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RatedTravel.Data.DataModels;

namespace RatedTravel.Core.Interfaces
{
    public interface IEmployeeService
    {
        Task<bool> EmployeeExistsByIdAndHasMoreThanThreeCreatedItemsAsync(string userId);
        Task<bool> EmployeeExistsByIdAsync(string userId);
        Task<Employee> EmployeeIdAsync(string userId);
        Task<bool> EmployeeExistsByPhoneNumberAsync(string phoneNumber);
        Task<bool> EmployeeExistsByNameAsync(string name);
        Task CreateEmployeeAsync(string userId, BecomeEmployeeFormModel model);
    }
}
