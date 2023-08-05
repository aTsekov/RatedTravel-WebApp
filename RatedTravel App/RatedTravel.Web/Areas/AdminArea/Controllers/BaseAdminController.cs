using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static RatedTravel.Common.GeneralApplicationConstants;

namespace RatedTravel.App.Web.Areas.AdminArea.Controllers
{
	[Area(AdminAreaName)]
	[Authorize(Roles = AdminRoleName)]
	public class BaseAdminController : Controller
	{
		
	}
}
