using doccure.Data;
using doccure.Data.Models;
using doccure.Data.RequestModels;
using doccure.Repositories.Interfance;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace doccure.Repositories.Implementaion
{
	public class ForgetPasswordService : IForgetPassword
	{
		private readonly IMailService mailService;
		private readonly SignInManager<Applicationuser> signInManager;
		private readonly UserManager<Applicationuser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;

		public ForgetPasswordService(IMailService mailService, SignInManager<Applicationuser> signInManager, UserManager<Applicationuser> userManager, RoleManager<IdentityRole> roleManager) {
			this.mailService = mailService;
			this.signInManager = signInManager;
			this.userManager = userManager;
			this.roleManager = roleManager;
			

		}
		public async Task<bool> SendToken(ForgetPasswordTokenRequest s)
		{
			var userExists = await userManager.FindByEmailAsync(s.Email);
			if(userExists== null) { return false; }
			else
			{
				var guid = Guid.NewGuid().ToString();
				userExists.ForgetPasswordToken = guid;
				var mail = await mailService.SendMailAsync(new MailDataRequest { EmailToId = userExists.Email, EmailSubject = "Token send to user", EmailToName = userExists.FirstName,EmailBody="Confirmation Token send "+ guid });
				if (mail)
				{
					var result = await userManager.UpdateAsync(userExists);
				}
				else
				{
					return false;
				}
				
				return true;

			}
		}

		public async Task<Applicationuser> UpdatePassword(ForgetPasswordRequest forgetPassword)
		{
			var UserData = await userManager.FindByEmailAsync(forgetPassword.Email);
			if (UserData == null)
			{
				UserData = null;
			}
			else
			{
				if (forgetPassword.Password != forgetPassword.ConfimPassword &&UserData.ForgetPasswordToken!=forgetPassword.Token)
				{
					UserData = null;
				}
				else
				{

					UserData.PasswordHash = userManager.PasswordHasher.HashPassword(UserData,forgetPassword.Password);
					UserData.ForgetPasswordToken = null;
					var result = await userManager.UpdateAsync(UserData);
					if (!result.Succeeded)
						{
							UserData = null;
						}


					
				}
			}
			return UserData;
		}
	}
}
