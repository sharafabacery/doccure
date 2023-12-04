using doccure.Data;
using doccure.Data.Models;
using doccure.Data.RequestModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace doccure.Repositories.Implementaion
{
	public class DoctorProfileSettingsService : IUserProfileSettingsService
	{
		private readonly UserManager<Applicationuser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly ApplicationDbContext applicationDbContext;

		public DoctorProfileSettingsService(UserManager<Applicationuser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext applicationDbContext)
		{
			this.userManager = userManager;
			this.roleManager = roleManager;
			this.applicationDbContext = applicationDbContext;
		}
		public async Task<Applicationuser> GetUserData(ClaimsPrincipal user)
		{
			var Doctor = await userManager.Users.Include(a => a.address).Include(a=>a.doctor).Include(a=>a.doctor.educations).Include(a=>a.doctor.experiences).Include(a=>a.doctor.awards).Include(a=>a.doctor.memberships).Include(a=>a.doctor.clinics).FirstOrDefaultAsync(usr => usr.Id == userManager.GetUserId(user));
			return Doctor;
			//throw new NotImplementedException();
		}

		public Task<Applicationuser> UpdatePassword(UpdatePasswordRequset updatePasswordRequset, ClaimsPrincipal userClamis)
		{
			throw new NotImplementedException();
		}

		public async Task<Applicationuser> UpdateUserData(object doctorUpdates, ClaimsPrincipal user)
		{
			DoctorProfileRequest doctorUpdatess = (DoctorProfileRequest)doctorUpdates;
			var Doctor = await userManager.Users.Include(a => a.address).Include(a => a.doctor).Include(a => a.doctor.educations).Include(a => a.doctor.experiences).Include(a => a.doctor.awards).Include(a => a.doctor.memberships).Include(a => a.doctor.clinics).FirstOrDefaultAsync(usr => usr.Id == userManager.GetUserId(user));
			Doctor.FirstName = doctorUpdatess.FirstName;
			Doctor.LastName = doctorUpdatess.LastName;
			Doctor.UserName= doctorUpdatess.UserName;
			Doctor.Email = doctorUpdatess.Email;
			Doctor.DateofBirth = doctorUpdatess.DateofBirth;
			Doctor.PhoneNumber = doctorUpdatess.PhoneNmber;
			Doctor.Gender= doctorUpdatess.gender=='1'?true:false;
			if (Doctor.address != null)
			{
				Doctor.address = new Address() { 
				Address1=doctorUpdatess.Address1.Address1,
				Address2= doctorUpdatess.Address1.Address2,
				City=doctorUpdatess.Address1.City,
				Country=doctorUpdatess.Address1.Country,
				PostalCode=doctorUpdatess.Address1.PostalCode,
				State=doctorUpdatess.Address1.State
				};
			}
			else
			{
				Doctor.address.Address1 = doctorUpdatess.Address1.Address1;
				Doctor.address.Address2 = doctorUpdatess.Address1.Address2;
				Doctor.address.City= doctorUpdatess.Address1.City;
				Doctor.address.Country= doctorUpdatess.Address1.Country;
				Doctor.address.PostalCode= doctorUpdatess.Address1.PostalCode;
				Doctor.address.State= doctorUpdatess.Address1.State;
			}
			//Doctor.SaveAsync();
			if (Doctor.doctor != null)
			{
				
			}
			else
			{
				Doctor.doctor = new Doctor {
					SpecialityId=doctorUpdatess.SpecialityId,
					AboutMe=doctorUpdatess.AboutMe,
					Specialization=doctorUpdatess.Specialization,
					Services = doctorUpdatess.Services,
					Price=doctorUpdatess.price,
					VideoCall=doctorUpdatess.videocall,
				};

			}
			throw new NotImplementedException();

		}
	}
}
