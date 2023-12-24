using doccure.Data.Models;
using doccure.Data.RequestModels;
using System.Security.Claims;

namespace doccure.Repositories.Interfance
{
	public interface IScheduleTimingService
	{
		public Task<Applicationuser> AddTimingSlot(ScheduleTimingSlotRequest scheduleTimingSlotRequest, ClaimsPrincipal user);
		public Task<List<ScheduleTiming>> UpdateTimingSlot(ScheduleTimingSlotRequest scheduleTimingSlotRequest, ClaimsPrincipal user);
		public Task<List<ScheduleTiming>> GetTimingSlotofDay(int Day, int clinicId, ClaimsPrincipal user);
		public Task<Applicationuser> GetTimingSlotofDoctor(ClaimsPrincipal user);
		public Task<List<ScheduleTiming>> GetTimingSlotByClinicId(int ClinicId, ClaimsPrincipal user);
		public Task<bool> DeleteTimingSlotById(int Id,ClaimsPrincipal user);

	}
}
