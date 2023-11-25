using doccure.Data.RequestModels;
using doccure.Repositories.Implementaion;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace doccure.Controllers.patient
{
    public class PatientAuthunticationController : Controller
    {

        private readonly IUserAuthenticationService authenticationService;

        public PatientAuthunticationController(IUserAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(registerRequest);
            }
            registerRequest.Role = "patient";
            bool result = await authenticationService.RegisterAsync(registerRequest);
            if (result)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction(nameof(Register));
            }


        }

        public IActionResult Login()
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
				
				return RedirectToAction(nameof(Login));
			}
		}
		[Authorize]
		public async Task<IActionResult> Logout()
		{
			await authenticationService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
	}
}
