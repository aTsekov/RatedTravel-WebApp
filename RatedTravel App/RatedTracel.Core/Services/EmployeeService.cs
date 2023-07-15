using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using RatedTravel.Core.Interfaces;
using RatedTravel.Data;
using RatedTravel.Data.DataModels;
using RatedTravel.Web.ViewModels.Employee;

namespace RatedTravel.Core.Services
{
    public class EmployeeService :IEmployeeService
    {
        private readonly RatedTravelDbContext dbContext;

        public EmployeeService(RatedTravelDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<bool> EmployeeExistsByIdAndHasMoreThanThreeCreatedItemsAsync(string userId)
        {
            //You can become an employee only if you are not an employee already and if you have created at least 3 Items

            bool isAlreadyEmployee = await this.dbContext.Employees.AnyAsync(e => e.UserId.ToString() == userId);
            int counter = await this.dbContext.Attractions.Where(c => c.IsActive == true).CountAsync(e => e.UserId.ToString() == userId);
            counter += await this.dbContext.Bars.Where(c => c.IsActive == true).CountAsync(b => b.UserId.ToString() == userId);
            counter += await this.dbContext.Restaurants.Where(c => c.IsActive == true).CountAsync(r => r.UserId.ToString() == userId);

            bool isValidToBecomeEmployee = !isAlreadyEmployee && counter >=3;

            return isValidToBecomeEmployee;
        }

        public async Task<bool> EmployeeExistsByIdAsync(string userId)
        {
			bool isAlreadyEmployee = await this.dbContext.Employees.AnyAsync(e => e.Id.ToString() == userId);

			return isAlreadyEmployee;
        }

        public async Task<Employee> EmployeeByUserIdAsync(string userId)
        {
            userId.ToUpper();
	        var employee = await  dbContext.Employees.FirstOrDefaultAsync(e => e.UserId.ToString() == userId);

	        return employee;
        }

        public async Task<bool> EmployeeExistsByPhoneNumberAsync(string phoneNumber)
        {
	        bool isPhoneNumberAlreadyUsed = await this.dbContext.Employees.AnyAsync(e => e.PhoneNumber == phoneNumber);

	        return isPhoneNumberAlreadyUsed;
        }

        public async Task<bool> EmployeeExistsByNameAsync(string name)
        {
	        bool isNameAlreadyUsed =  await this.dbContext.Employees.AnyAsync( e=> e.FullName == name);

            return isNameAlreadyUsed;
        }

        public async Task CreateEmployeeAsync(string userId, BecomeEmployeeFormModel model)
        {
	        Employee empl = new Employee()
	        {
		        FullName = model.FullName,
		        PhoneNumber = model.PhoneNumber,
		        UserId = Guid.Parse(userId),
	        };
           await this.dbContext.Employees.AddAsync(empl);
           await this.dbContext.SaveChangesAsync();

        }

        public async Task<bool> EmployeeExistsByUserIdAsync(string userId)
        {
            bool result = await this.dbContext
                .Employees
                .AnyAsync(a => a.UserId.ToString() == userId);

            return result;
        }
    }
}
