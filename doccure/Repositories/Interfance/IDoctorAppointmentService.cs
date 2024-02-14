using doccure.Data.Models;
using System.Security.Claims;

namespace doccure.Repositories.Interfance
{
	public interface IDoctorAppointmentService
	{
		public Task<List<Booking>> GetDoctorAppointmentWithinWeek(ClaimsPrincipal claims);
	}
}
