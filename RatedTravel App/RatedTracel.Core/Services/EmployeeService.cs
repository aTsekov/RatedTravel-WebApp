using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using RatedTravel.Core.Interfaces;
using RatedTravel.Data;

namespace RatedTravel.Core.Services
{
    public class EmployeeService :IEmploeeService
    {
        private readonly RatedTravelDbContext dbContext;

        public EmployeeService(RatedTravelDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<bool> AgentExistsByIdAndHasMoreThanThreeCreatedItems(string userId)
        {
            //You can become an employee only if you are not an employee already and if you have created at least 3 Items

            bool isAlreadyEmployee = await this.dbContext.Employees.AnyAsync(a => a.UserId.ToString() == userId);
            int counter = await this.dbContext.Attractions.CountAsync(a => a.UserId.ToString() == userId);
            counter += await this.dbContext.Bars.CountAsync(b => b.UserId.ToString() == userId);
            counter += await this.dbContext.Restaurants.CountAsync(r => r.UserId.ToString() == userId);

            bool isValidToBecomeEmployee = !isAlreadyEmployee && counter >=3;

            return isValidToBecomeEmployee;
        }
    }
}
