using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Caching.Memory;
using RatedTravel.Core.Interfaces;
using RatedTravel.Web.ViewModels.User;

namespace RatedTravel.App.Web.Areas.AdminArea.Controllers
{
	public class UserController : BaseAdminController
	{
		private readonly IUserService userService;
		private readonly IMemoryCache memoryCache;

		public UserController(IUserService userService, IMemoryCache memoryCache)
		{
			this.userService = userService;
			this.memoryCache = memoryCache;
		}

		[Route("User/All")]
		[ResponseCache(Duration = 30, Location = ResponseCacheLocation.Client, NoStore = false)]
		public async Task<IActionResult> All()
		{
			IEnumerable<UserViewModel> users = await this.userService.AllAsync();
			return View(users);
		}
	}
}
	

