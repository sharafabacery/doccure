using doccure.Data.Models;
using System.Security.Claims;

namespace doccure.Repositories.Interfance
{
	public interface IFavouritesServcie
	{
		public Task<bool> CreateFavouriteDoctor(ClaimsPrincipal claims, int DoctorId);
		public Task<bool> DeleteFavouriteDoctor(ClaimsPrincipal claims, int DoctorId);
		public Task<List<Favourites>> GetFavouriteDoctorsByPatientId(ClaimsPrincipal claims);

	}
}
