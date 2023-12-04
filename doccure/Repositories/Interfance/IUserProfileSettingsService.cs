using doccure.Data.Models;
using doccure.Data.RequestModels;
using System.Security.Claims;

namespace doccure.Repositories.Interfance
{
	public interface IUserProfileSettingsService
	{
		Task<Applicationuser> GetUserData(ClaimsPrincipal user);
		Task<Applicationuser> UpdateUserData(object user, ClaimsPrincipal userClamis);

		Task<Applicationuser> UpdatePassword(UpdatePasswordRequset updatePasswordRequset,ClaimsPrincipal userClamis);
	}
}
