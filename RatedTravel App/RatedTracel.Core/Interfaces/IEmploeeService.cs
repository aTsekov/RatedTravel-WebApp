using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatedTravel.Core.Interfaces
{
    public interface IEmploeeService
    {
        Task<bool> AgentExistsByIdAndHasMoreThanThreeCreatedItems(string userId);
    }
}
