using doccure.Data.Models;
using doccure.Data.RequestModels;
using doccure.Repositories.Interfance;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Net.Mail;

namespace doccure.Repositories.Implementaion
{
	public class ForgetPasswordService : IForgetPassword
	{
		private readonly ConfigurationManager configuration;
		private readonly SmtpClient smtpClient;
		private readonly SignInManager<Applicationuser> signInManager;
		private readonly UserManager<Applicationuser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;

		public ForgetPasswordService(ConfigurationManager configuration,SmtpClient smtpClient, SignInManager<Applicationuser> signInManager, UserManager<Applicationuser> userManager, RoleManager<IdentityRole> roleManager) {
			this.configuration = configuration;
			this.smtpClient = smtpClient;
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
				Guid guid = new Guid();
				string guids = guid.ToString().Split('-')[0];
				userExists.ForgetPasswordToken = guids;
				var client = new SmtpClient(configuration.GetValue<string>("Host"), configuration.GetValue<int>("Port"))
				{
					Credentials = new NetworkCredential(configuration.GetValue<string>("Username"), configuration.GetValue<string>("Password")),
					EnableSsl = true
				};
				MailMessage message = new MailMessage(configuration.GetValue<string>("SenderEmail"), s.Email);
				message.Subject = "Confirmation Token";
				message.Body = "this token used to make new password " + guids;
				await client.SendMailAsync(message);
				var result = await userManager.UpdateAsync(userExists);
				return true;

			}
		}
	}
}
