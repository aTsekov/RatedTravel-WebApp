using System.Security.Claims;
using static RatedTravel.Common.GeneralApplicationConstants;

namespace RatedTravel.App.Web.Infrastructure
{
    public class ClaimsPrincipleExtensions
    {
        public static bool IsAdmin(ClaimsPrincipal user)
        {
            return user.IsInRole(AdminRoleName);
        }
    }
}
