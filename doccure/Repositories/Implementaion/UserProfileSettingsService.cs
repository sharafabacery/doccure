using doccure.Data;
using doccure.Data.Models;
using doccure.Data.RequestModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static doccure.Program;

namespace doccure.Repositories.Implementaion
{

	public class UserProfileSettingsService : IUserProfileSettingsService
	{
		private readonly UserManager<Applicationuser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly ApplicationDbContext applicationDbContext;
		private readonly IFileService fileService;

		public UserProfileSettingsService(ServiceResolver2 serviceAccessor, UserManager<Applicationuser> userManager, RoleManager<IdentityRole> roleManager,ApplicationDbContext applicationDbContext)
		{
			this.userManager = userManager;
			this.roleManager = roleManager;
			this.applicationDbContext = applicationDbContext;
			this.fileService = serviceAccessor("Image"); 
		}
		public async Task<Applicationuser> GetUserData(ClaimsPrincipal user)
		{
			var UserData = await userManager.Users.Include(a => a.address).Include(f=>f.PatientFavourites).FirstOrDefaultAsync(usr => usr.Id == userManager.GetUserId(user));
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
			var UserData= await userManager.Users.Include(a => a.address).FirstOrDefaultAsync(usr => usr.Id == userManager.GetUserId(userClamis));
			if (UserData == null)
			{
				return null;
			}
			else
			{
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

				if (UserData.address == null)
				{
					UserData.address = new Address()
					{
						Address1 = user.Address1.Address1,
						City = user.Address1.City,
						Country = user.Address1.Country,
						PostalCode = user.Address1.PostalCode,
						State = user.Address1.State
					};
				}
				else
				{
					UserData.address.Address1 = user.Address1.Address1;
					UserData.address.City = user.Address1.City;
					UserData.address.Country = user.Address1.Country;
					UserData.address.PostalCode = user.Address1.PostalCode;
					UserData.address.State = user.Address1.State;
				}

				var result=	await userManager.UpdateAsync(UserData);
				
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
