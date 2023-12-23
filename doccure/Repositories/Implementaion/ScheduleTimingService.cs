using doccure.Data.Models;
using doccure.Data;
using doccure.Data.RequestModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
			var User = await userManager.Users
				.Include(c => c.doctor)
				//.Include(c=> c.doctor.clinics.FirstOrDefault().scheduleTiming.Where(e => e.Day == scheduleTimingSlotRequest.Day))
				.FirstOrDefaultAsync(usr => usr.Id == userManager.GetUserId(user));
			if(User == null||User.doctor==null)
			{
				return null;
			}
			var Clinic=await applicationDbContext.Clinics.Include(c => c.scheduleTiming).FirstOrDefaultAsync(c=>c.doctor.Id==User.doctor.Id);
			if (Clinic == null)
			{
				return null;
			}
			foreach (var s in scheduleTimingSlotRequest.scheduleTimings)
			{
				Clinic.scheduleTiming.Add(s);
				//UserSlots.doctor.clinics.FirstOrDefault().scheduleTiming.Add(s);
			}
			var result = await applicationDbContext.SaveChangesAsync();
			if (result==1)
			{
				return User;
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

		public async Task<List<ScheduleTiming>> GetTimingSlotByClinicId(int ClinicId, ClaimsPrincipal user)
		{
			var UserSlots = await applicationDbContext.ScheduleTiming.FromSql($"SELECT S.*\r\nFROM  [dbo].[Doctor] D\r\nLEFT JOIN [dbo].[Clinics] C\r\nON C.DoctorId=D.Id\r\nLEFT JOIN [dbo].[ScheduleTiming] S\r\nON S.ClinicId=C.Id\r\nWHERE D.applicationuserId={userManager.GetUserId(user)} AND C.Id={ClinicId} ORDER BY S.Day,S.StartTime")
																	.ToListAsync();
			//UserSlots.doctor.clinics.Where(c => c.Id == ClinicId);
			return UserSlots;
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
				.Include(c => c.doctor.Speciality)
				.Include(c => c.doctor.clinics)
				.ThenInclude(c => c.scheduleTiming)
				.FirstOrDefaultAsync(usr => usr.Id == userManager.GetUserId(user));

				UserSlots.doctor?.clinics?.OrderBy(e => e.scheduleTiming.OrderBy(c=>c.Day));
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
