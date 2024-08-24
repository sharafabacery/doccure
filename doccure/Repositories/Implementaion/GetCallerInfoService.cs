using doccure.Data;
using doccure.Data.Models;
using doccure.Data.ResponseModels;
using doccure.Repositories.Interfance;
using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace doccure.Repositories.Implementaion
{
	public class GetCallerInfoService : IGetCallerInfoService
	{
		private readonly UserManager<Applicationuser> userManager;
		private readonly ApplicationDbContext applicationDbContext;

		public GetCallerInfoService(UserManager<Applicationuser> userManager, ApplicationDbContext applicationDbContext)
		{
			this.userManager = userManager;
			this.applicationDbContext = applicationDbContext;
		}
		public async Task<UserDTO> GetCallerInfo(ClaimsPrincipal claims, string meetingId)
		{
			string id = userManager.GetUserId(claims);
			var caller = await applicationDbContext.Groups.Include(e => e.user)
														.Where(e => e.Name == meetingId)
														.Select(e => new UserDTO()
														{
															Id = e.user.Where(e => e.Id != id).FirstOrDefault().Id,
															FullName= e.user.Where(e => e.Id != id).FirstOrDefault().FirstName+' '+ e.user.Where(e => e.Id != id).FirstOrDefault().LastName,
															ProfileImage= e.user.Where(e => e.Id != id).FirstOrDefault().Image
														}).FirstOrDefaultAsync();
			return caller;

		}

		public async Task<UserDTO> GetUser(ClaimsPrincipal claims)
		{
			var user=await userManager.GetUserAsync(claims);
			if (user == null)
			{
				return null;
			}
			else
			{
				var userDTO = new UserDTO();
				userDTO.Id = user.Id;
				userDTO.ProfileImage = user.Image;
				userDTO.FullName = user.FirstName + ' ' + user.LastName;
				return userDTO;
			}
		}
	}
}
