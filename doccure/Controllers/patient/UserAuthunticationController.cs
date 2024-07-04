using doccure.Data.RequestModels;
using doccure.Repositories.Implementaion;
using doccure.Repositories.Interfance;
using Google.Apis.Auth;
using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace doccure.Controllers.patient
{
    public class UserAuthunticationController : Controller
    {
		private readonly IGoogleAuthProvider googleAuth;
		private readonly IUserAuthenticationService authenticationService;
		private readonly IForgetPassword forgetPassword;
		//private readonly IGoogleAuthProvider google;
		public UserAuthunticationController(IGoogleAuthProvider googleAuth, IUserAuthenticationService authenticationService,IForgetPassword forgetPassword)
        {
			this.googleAuth = googleAuth;
			this.authenticationService = authenticationService;
			this.forgetPassword = forgetPassword;
			//this.google = google;
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
		
		public async Task<IActionResult> LoginGoogle(string credential,string type)
		{
			GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(credential);
			if (payload == null)
			{
				return RedirectToAction(nameof(Login));
			}
			var result = await authenticationService.RegisterExtnal(new ExternalLoginRequestcs { Email = payload.Email,UserName=payload.Name, Name = payload.GivenName, FamilyName =payload.FamilyName,Picture=payload.Picture,Type=type });
			if (result)
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				var clamisUser=await authenticationService.LoginExtnal(new ExternalLoginRequestcs { Email = payload.Email, UserName = payload.Name, Name = payload.GivenName, FamilyName = payload.FamilyName, Picture = payload.Picture, Type = type });
				if (!clamisUser)
				{
					return RedirectToAction(nameof(Login));
				}
				else {

					return RedirectToAction("Index", "Home"); }
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
