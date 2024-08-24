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
			var id =await userManager.GetUserAsync(claims);
			var callerr = await applicationDbContext.Groups
														.Where(e => e.Name == meetingId)
														.FirstOrDefaultAsync();
			if(callerr == null)
			{
				return null;
			}
			else
			{

				var caller = await applicationDbContext.UserGroups.Include(e => e.applicationuser).Where(e => e.group.Equals(callerr)&&!e.applicationuser.Equals(id)).Select(e => e.applicationuser).FirstOrDefaultAsync();
				if(caller == null)
				{
					return null;
				}
				var userdto = new UserDTO() { FullName = caller.FirstName + ' ' + caller.LastName,ProfileImage=caller.Image,Id=caller.Id };
				return userdto;
			}

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
