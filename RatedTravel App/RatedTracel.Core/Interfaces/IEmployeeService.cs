using RatedTravel.Web.ViewModels.Employee;
using RatedTravel.Data.DataModels;

namespace RatedTravel.Core.Interfaces
{
    public interface IEmployeeService
    {
        Task<bool> EmployeeExistsByIdAndHasMoreThanThreeCreatedItemsAsync(string userId);
        Task<bool> EmployeeExistsByIdAsync(string userId);
        Task<Employee> EmployeeByUserIdAsync(string userId);
        Task<bool> EmployeeExistsByPhoneNumberAsync(string phoneNumber);
        Task<bool> EmployeeExistsByNameAsync(string name);
        Task CreateEmployeeAsync(string userId, BecomeEmployeeFormModel model);
        Task<bool> EmployeeExistsByUserIdAsync(string userId);
    }
}
