using System.Security.Claims;

namespace doccure.Repositories.Interfance
{
	public interface IDeleteMembershipService
	{
		public Task<bool> DeleteMembershipAsync(int id, ClaimsPrincipal user);
	}
}
