using doccure.Data.Models;
using doccure.Data;
using doccure.Data.RequestModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace doccure.Repositories.Implementaion
{
	public class ScheduleTimingService : IScheduleTimingService
	{
		private readonly UserManager<Applicationuser> userManager;
		private readonly ApplicationDbContext applicationDbContext;

		public ScheduleTimingService(UserManager<Applicationuser> userManager, ApplicationDbContext applicationDbContext)
		{
			this.userManager = userManager;
			this.applicationDbContext = applicationDbContext;
		}
		public async Task<Applicationuser> AddTimingSlot(ScheduleTimingSlotRequest scheduleTimingSlotRequest, ClaimsPrincipal user)
		{
			var UserSlots = await userManager.Users
				.Include(c => c.doctor)
				.Include(c=> c.doctor.clinics.FirstOrDefault().scheduleTiming.Where(e => e.Day == scheduleTimingSlotRequest.Day))
				.FirstOrDefaultAsync(usr => usr.Id == userManager.GetUserId(user));
			if(UserSlots == null)
			{
				return null;
			}
			foreach (var s in scheduleTimingSlotRequest.scheduleTimings)
			{
				UserSlots.doctor.clinics.FirstOrDefault().scheduleTiming.Add(s);
			}
			var  result = await userManager.UpdateAsync(UserSlots);
			if (result.Succeeded)
			{
				return UserSlots;
			}
			else
			{
				return null;
			}
		}

		public async Task<bool> DeleteTimingSlotById(int Id, ClaimsPrincipal user)
		{
			var User = await userManager.Users.Include(a => a.doctor)
									.Include(a=>a.doctor.clinics)
									.FirstOrDefaultAsync(usr => usr.Id == userManager.GetUserId(user));
			var slot = await applicationDbContext.ScheduleTiming.FirstOrDefaultAsync(e => e.Id == Id);
			if (User == null || slot == null)
			{
				return false;
			}
			else
			{
				if (slot.ClinicId == User.doctor.clinics.FirstOrDefault().Id)
				{
					applicationDbContext.Remove(slot);
					await applicationDbContext.SaveChangesAsync();
					return true;
				}
				else
				{
					return false;
				}


			}
		}

		public async Task<Applicationuser> GetTimingSlotofDay(int day, ClaimsPrincipal user)
		{
			var UserSlots = await userManager.Users
				.Include(c => c.doctor)
				.Include(c => c.doctor.clinics.FirstOrDefault().scheduleTiming.Where(e => e.Day == day))
				.FirstOrDefaultAsync(usr => usr.Id == userManager.GetUserId(user));
			return UserSlots;
		}

		public async Task<Applicationuser> GetTimingSlotofDoctor(ClaimsPrincipal user)
		{
			var UserSlots = await userManager.Users
				.Include(c => c.doctor)
				.Include(c => c.doctor.clinics)
				.ThenInclude(c=>c.scheduleTiming)
				//.OrderBy(c=>c.doctor.clinics.s)
				//.Include(c => c.doctor.clinics.FirstOrDefault().scheduleTiming.GroupBy(e=>e.Day))
				.FirstOrDefaultAsync(usr => usr.Id == userManager.GetUserId(user))
				//.GroupBy(c=>c.doctor.clinics.FirstOrDefault().scheduleTiming)
				;
			return UserSlots;
		}

		public async Task<Applicationuser> UpdateTimingSlot(ScheduleTimingSlotRequest scheduleTimingSlotRequest, ClaimsPrincipal user)
		{
			var UserSlots = await userManager.Users
				.Include(c => c.doctor)
				.Include(c => c.doctor.clinics.FirstOrDefault().scheduleTiming.Where(e => e.Day == scheduleTimingSlotRequest.Day))
				.FirstOrDefaultAsync(usr => usr.Id == userManager.GetUserId(user));
			if (UserSlots == null)
			{
				return null;
			}
			foreach (var s in scheduleTimingSlotRequest.scheduleTimings)
			{
				var slot = UserSlots.doctor.clinics.FirstOrDefault().scheduleTiming.FirstOrDefault(e=>e.Id==s.Id);
				if (slot != null)
				{
					slot.StartTime=s.StartTime;
					slot.EndTime=s.EndTime;
				}
			}
			var result = await userManager.UpdateAsync(UserSlots);
			if (result.Succeeded)
			{
				return UserSlots;
			}
			else
			{
				return null;
			}
		}
	}
}
