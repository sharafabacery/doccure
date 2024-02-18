using doccure.Data.ResponseModels;
using System.Security.Claims;

namespace doccure.Repositories.Interfance
{
	public interface IDoctorDashboardService
	{
		public Task<DoctorDashboardReturned> DoctorDashboardData(ClaimsPrincipal claims);
	}
}
