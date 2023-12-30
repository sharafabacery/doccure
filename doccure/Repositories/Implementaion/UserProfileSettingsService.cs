using doccure.Data;
using doccure.Data.Models;
using doccure.Data.RequestModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace doccure.Repositories.Implementaion
{

	public class UserProfileSettingsService : IUserProfileSettingsService
	{
		private readonly UserManager<Applicationuser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly ApplicationDbContext applicationDbContext;
		private readonly IFileService fileService;

		public UserProfileSettingsService(UserManager<Applicationuser> userManager, RoleManager<IdentityRole> roleManager,ApplicationDbContext applicationDbContext,IFileService fileService)
		{
			this.userManager = userManager;
			this.roleManager = roleManager;
			this.applicationDbContext = applicationDbContext;
			this.fileService = fileService;
		}
		public async Task<Applicationuser> GetUserData(ClaimsPrincipal user)
		{
			var UserData = await userManager.Users.Include(a => a.address).FirstOrDefaultAsync(usr => usr.Id == userManager.GetUserId(user));
			return UserData;

		}

		public async Task<Applicationuser> UpdatePassword(UpdatePasswordRequset updatePasswordRequset, ClaimsPrincipal userClamis)
		{
			var UserData = await userManager.GetUserAsync(userClamis);
			if (UserData == null)
			{
				UserData= null;
			}
			else
			{
				if (updatePasswordRequset.ConfirmPassword != updatePasswordRequset.NewPassword)
				{
					UserData = null;
				}
				else
				{
					bool IsPassword = await userManager.CheckPasswordAsync(UserData, updatePasswordRequset.OldPassword);
					if (IsPassword)
					{
						var usernewPassword = await userManager.ChangePasswordAsync(UserData, updatePasswordRequset.OldPassword, updatePasswordRequset.NewPassword);
						if (!usernewPassword.Succeeded)
						{
							UserData = null;
						}
						

					}
					else
					{
						UserData = null;
					}
				}
			}
			return UserData;
		}

		public async Task<Applicationuser> UpdateUserData(object userr, ClaimsPrincipal userClamis)
		{
			var user = (UserProfileRequest)userr;
			var UserData = await userManager.GetUserAsync(userClamis);
			if(UserData == null)
			{
				return null;
			}
			else
			{
				var userAddress =  await applicationDbContext.Address.FirstOrDefaultAsync(e => e.applicationuserId == userManager.GetUserId(userClamis));
			UserData.FirstName = user.FirstName;
			UserData.LastName=user.LastName;
			UserData.Email = user.Email;
			UserData.PhoneNumber = user.PhoneNmber;
			UserData.DateofBirth = user.DateofBirth;
			UserData.BloodGroup = user.BloodGroup;
			string UserImage = fileService.SaveFile(user.ImageFile);
				if (UserImage == "")
				{
					UserData.Image = UserData.Image;
				}
				else
				{
					UserData.Image = UserImage;
				}

			if (userAddress != null)
			{

					userAddress.Address1 = user.Address1.Address1;
				userAddress.City = user.Address1.City;
				userAddress.Country = user.Address1.Country;
				userAddress.PostalCode = user.Address1.PostalCode;
				userAddress.State = user.Address1.State;
					//applicationDbContext.Address.Update(userAddress);

			}
			else
			{
					userAddress = user.Address1;
			}

			var result=	await userManager.UpdateAsync(UserData);
				applicationDbContext.Address.Update(userAddress);
				if (result.Succeeded)
				{
					return UserData;
				}
				else
				{
					return null;
				}
			}
			
			

		}
	}
}
