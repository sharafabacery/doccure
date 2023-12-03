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

		public UserProfileSettingsService(UserManager<Applicationuser> userManager, RoleManager<IdentityRole> roleManager,ApplicationDbContext applicationDbContext)
		{
			this.userManager = userManager;
			this.roleManager = roleManager;
			this.applicationDbContext = applicationDbContext;
		}
		public async Task<Applicationuser> GetUserData(ClaimsPrincipal user)
		{
			var UserData =await  userManager.GetUserAsync(user);
			var userAddress = await applicationDbContext.Address.FirstOrDefaultAsync(e => e.applicationuserId == UserData.Id);
			UserData.address = userAddress;
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

		public async Task<Applicationuser> UpdateUserData(UserProfileRequest user, ClaimsPrincipal userClamis)
		{
			var UserData = await userManager.GetUserAsync(userClamis);
			if(UserData == null)
			{
				return null;
			}
			else
			{
				var userAddress =  await applicationDbContext.Address.FirstOrDefaultAsync(e => e.applicationuserId == UserData.Id);
			UserData.FirstName = user.FirstName;
			UserData.LastName=user.LastName;
			UserData.Email = user.Email;
			UserData.PhoneNumber = user.PhoneNmber;
			UserData.DateofBirth = user.DateofBirth;
			UserData.BloodGroup = user.BloodGroup;
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
