using doccure.Data.RequestModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Mvc;

namespace doccure.Areas.AdminCorner.AdminCorner.Controllers
{
	[Area("AdminCorner")]
	public class AuthunticationController : Controller
	{
		private readonly IUserAuthenticationService authenticationService;

		public AuthunticationController(IUserAuthenticationService authenticationService)
		{
			this.authenticationService = authenticationService;
		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginRequest loginModel)
		{
			if (!ModelState.IsValid)
			{
				return View(loginModel);
			}
			var result = await authenticationService.LoginAsync(loginModel);
			if (result)
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{

				return RedirectToAction(nameof(Index));
			}
		}
	}
}
