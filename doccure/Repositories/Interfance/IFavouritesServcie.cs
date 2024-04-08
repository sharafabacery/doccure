using doccure.Data.Models;
using System.Security.Claims;

namespace doccure.Repositories.Interfance
{
	public interface IFavouritesServcie
	{
		public Task<bool> CreateFavouriteDoctor(ClaimsPrincipal claims, string DoctorId);
		public Task<bool> DeleteFavouriteDoctor(ClaimsPrincipal claims, string DoctorId);
		public Task<List<Favourites>> GetFavouriteDoctorsByPatientId(ClaimsPrincipal claims);

	}
}
