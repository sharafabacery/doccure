using doccure.Data.ResponseModels;
using System.Security.Claims;

namespace doccure.Repositories.Interfance
{
	public interface IGetCallerInfoService
	{
		public Task<UserDTO> GetCallerInfo(ClaimsPrincipal claims,string meetingId);
		public Task<UserDTO> GetUser(ClaimsPrincipal claims);
	}
}
