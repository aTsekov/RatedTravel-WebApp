using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static RaterTravel.Common.GeneralApplicationConstants;

namespace RatedTravel.App.Web.Areas.AdminArea.Controllers
{
	[Area(AreaName)]
	[Authorize(Roles = AdminRoleName)]
	public class BaseAdminController : Controller
	{
		
	}
}
