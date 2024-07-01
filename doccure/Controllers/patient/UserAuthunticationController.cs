using doccure.Data.RequestModels;
using doccure.Repositories.Implementaion;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace doccure.Controllers.patient
{
    public class UserAuthunticationController : Controller
    {

        private readonly IUserAuthenticationService authenticationService;
		private readonly IForgetPassword forgetPassword;

		public UserAuthunticationController(IUserAuthenticationService authenticationService,IForgetPassword forgetPassword)
        {
            this.authenticationService = authenticationService;
			this.forgetPassword = forgetPassword;
		}
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult RegisterDoctor()
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
            bool result = await authenticationService.RegisterAsync(registerRequest);
            if (result)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.message = "Cant register the account";
                return View();
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
        public IActionResult ForgetPassword()
        {
            return View();
        }
		[HttpPost]
		public async Task<IActionResult> ForgetPassword(ForgetPasswordTokenRequest loginModel)
		{
			if (!ModelState.IsValid)
			{
				return View(loginModel);
			}
			var result = await forgetPassword.SendToken(loginModel);
			if (!result)
			{
				return RedirectToAction(nameof(ForgetPassword));
			}
			else
			{

				return RedirectToAction(nameof(ChangePassword));
			}
		}
		public IActionResult ChangePassword()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> ChangePasswordAsync(ForgetPasswordRequest loginModel)
		{
			if (!ModelState.IsValid)
			{
				return View(loginModel);
			}
			var result = await forgetPassword.UpdatePassword(loginModel);
			if (result==null)
			{
				ViewBag.msg = "error in update";
				return RedirectToAction(nameof(ForgetPassword));
			}
			else
			{

				return RedirectToAction(nameof(Login));
			}
		}
		public async Task<IActionResult> LoginGoogle()
		{
			var xx = User;
			await authenticationService.LogoutAsync();
			return RedirectToAction("Index", "Home");
		}
		[Authorize]
		public async Task<IActionResult> Logout()
		{
			await authenticationService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
	}
}
