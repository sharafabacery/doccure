using System.Security.Claims;

namespace doccure.Repositories.Interfance
{
	public interface IDeleteExperience
	{
		public Task<bool> DeleteExperienceAsync(int id, ClaimsPrincipal user);
	}
}
