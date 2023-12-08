using System.Security.Claims;

namespace doccure.Repositories.Interfance
{
	public interface IDeleteAwardService
	{
		public Task<bool> DeleteAwardAsync(int id, ClaimsPrincipal user);
	}
}
