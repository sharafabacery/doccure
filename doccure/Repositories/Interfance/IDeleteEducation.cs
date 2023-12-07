using System.Security.Claims;

namespace doccure.Repositories.Interfance
{
	public interface IDeleteEducation
	{
		public Task<bool> DeleteEducationAsync(int id, ClaimsPrincipal user);
	}
}
