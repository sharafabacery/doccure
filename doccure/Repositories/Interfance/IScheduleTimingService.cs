using doccure.Data.Models;
using doccure.Data.RequestModels;
using System.Security.Claims;

namespace doccure.Repositories.Interfance
{
	public interface IScheduleTimingService
	{
		public Task<Applicationuser> AddTimingSlot(ScheduleTimingSlotRequest scheduleTimingSlotRequest, ClaimsPrincipal user);
		public Task<Applicationuser> UpdateTimingSlot(ScheduleTimingSlotRequest scheduleTimingSlotRequest, ClaimsPrincipal user);
		public Task<Applicationuser> GetTimingSlotofDay(int Day, ClaimsPrincipal user);
		public Task<Applicationuser> GetTimingSlotofDoctor(ClaimsPrincipal user);
		public Task<bool> DeleteTimingSlotById(int Id,ClaimsPrincipal user);

	}
}
