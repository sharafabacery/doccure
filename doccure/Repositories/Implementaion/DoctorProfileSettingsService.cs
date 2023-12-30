using doccure.Data;
using doccure.Data.Models;
using doccure.Data.RequestModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Transactions;
using static System.Formats.Asn1.AsnWriter;

namespace doccure.Repositories.Implementaion
{
	public class DoctorProfileSettingsService : IUserProfileSettingsService
	{
		private readonly UserManager<Applicationuser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly ApplicationDbContext applicationDbContext;
		private readonly IFileService fileService;

		public DoctorProfileSettingsService(UserManager<Applicationuser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext applicationDbContext, IFileService fileService)
		{
			this.userManager = userManager;
			this.roleManager = roleManager;
			this.applicationDbContext = applicationDbContext;
			this.fileService = fileService;
		}
		public async Task<Applicationuser> GetUserData(ClaimsPrincipal user)
		{
			var Doctor = await userManager.Users.Include(a => a.address).Include(a=>a.doctor).Include(a=>a.doctor.educations).Include(a=>a.doctor.experiences).Include(a=>a.doctor.awards).Include(a=>a.doctor.memberships).Include(a=>a.doctor.clinics).FirstOrDefaultAsync(usr => usr.Id == userManager.GetUserId(user));
			return Doctor;
			//throw new NotImplementedException();
		}

		public async Task<Applicationuser> UpdatePassword(UpdatePasswordRequset updatePasswordRequset, ClaimsPrincipal userClamis)
		{
			var UserData = await userManager.GetUserAsync(userClamis);
			if (UserData == null)
			{
				UserData = null;
			}
			else
			{
				if (updatePasswordRequset.ConfirmPassword != updatePasswordRequset.NewPassword)
				{
					UserData = null;
				}
				else
				{
					bool IsPassword = await userManager.CheckPasswordAsync(UserData, updatePasswordRequset.OldPassword);
					if (IsPassword)
					{
						var usernewPassword = await userManager.ChangePasswordAsync(UserData, updatePasswordRequset.OldPassword, updatePasswordRequset.NewPassword);
						if (!usernewPassword.Succeeded)
						{
							UserData = null;
						}


					}
					else
					{
						UserData = null;
					}
				}
			}
			return UserData;
		}

		public async Task<Applicationuser> UpdateUserData(object doctorUpdates, ClaimsPrincipal user)
		{
			DoctorProfileRequest doctorUpdatess = (DoctorProfileRequest)doctorUpdates;
			var Doctor = await userManager.Users.Include(a => a.address).Include(a => a.doctor).Include(a => a.doctor.educations).Include(a => a.doctor.experiences).Include(a => a.doctor.awards).Include(a => a.doctor.memberships).Include(a => a.doctor.clinics).FirstOrDefaultAsync(usr => usr.Id == userManager.GetUserId(user));
				using(var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
			{
				try
				{
					Doctor.FirstName = doctorUpdatess.FirstName;
					Doctor.LastName = doctorUpdatess.LastName;
					Doctor.UserName = doctorUpdatess.UserName;
					Doctor.Email = doctorUpdatess.Email;
					Doctor.DateofBirth = doctorUpdatess.DateofBirth;
					Doctor.PhoneNumber = doctorUpdatess.PhoneNmber;
					Doctor.Gender = doctorUpdatess.gender == '1' ? true : false;
					string DoctorImage = fileService.SaveFile(doctorUpdatess.ImageFile);
					if (DoctorImage == "")
					{
						Doctor.Image = Doctor.Image;
					}
					else
					{
						Doctor.Image = DoctorImage;
					}

					if (Doctor.address == null)
					{
						Doctor.address = new Address()
						{
							Address1 = doctorUpdatess.Address1.Address1,
							Address2 = doctorUpdatess.Address1.Address2,
							City = doctorUpdatess.Address1.City,
							Country = doctorUpdatess.Address1.Country,
							PostalCode = doctorUpdatess.Address1.PostalCode,
							State = doctorUpdatess.Address1.State
						};
					}
					else
					{
						Doctor.address.Address1 = doctorUpdatess.Address1.Address1;
						Doctor.address.Address2 = doctorUpdatess.Address1.Address2;
						Doctor.address.City = doctorUpdatess.Address1.City;
						Doctor.address.Country = doctorUpdatess.Address1.Country;
						Doctor.address.PostalCode = doctorUpdatess.Address1.PostalCode;
						Doctor.address.State = doctorUpdatess.Address1.State;
					}
					var result = await userManager.UpdateAsync(Doctor);
					if (result.Succeeded)
					{
						if (Doctor.doctor == null)
						{
							Doctor.doctor = new Doctor
							{
								SpecialityId = doctorUpdatess.SpecialityId,
								AboutMe = doctorUpdatess.AboutMe,
								Specialization = doctorUpdatess.Specialization,
								Services = doctorUpdatess.Services,
								Price = doctorUpdatess.price,
								VideoCall = doctorUpdatess.videocall,
							};
						}
						else
						{
							Doctor.doctor.SpecialityId = doctorUpdatess.SpecialityId;
							Doctor.doctor.AboutMe = doctorUpdatess.AboutMe;
							Doctor.doctor.Specialization = doctorUpdatess.Specialization;
							Doctor.doctor.Services = doctorUpdatess.Services;
							Doctor.doctor.Price = doctorUpdatess.price;
							Doctor.doctor.VideoCall = doctorUpdatess.videocall;

						}
					}
					else
					{
						throw new Exception("can not update user profile");
					}
					result = await userManager.UpdateAsync(Doctor);
					if (result.Succeeded)
					{
						foreach(var education in doctorUpdatess.Educations)
						{
							var educationdb = Doctor.doctor.educations.FirstOrDefault(e => e.Id == education.Id);
							if (educationdb != null)
							{
								educationdb.Degree = education.Degree;
								educationdb.College= education.College;
								educationdb.YearofCompletion= education.YearofCompletion;
							}
							else
							{
								Doctor.doctor.educations.Add(education);
							}
						}
					}
					else
					{
						throw new Exception("can not update doctor profile");
					}
					result = await userManager.UpdateAsync(Doctor);
					if (result.Succeeded)
					{
						foreach (var experience in doctorUpdatess.Experience)
						{
							var experiencedb = Doctor.doctor.experiences.FirstOrDefault(e => e.Id == experience.Id);
							if (experiencedb != null)
							{
								experiencedb.From = experience.From;
								experiencedb.To = experience.To;
								experiencedb.HospitalName = experience.HospitalName;
								experiencedb.Designation = experience.Designation;

							}
							else
							{
								Doctor.doctor.experiences.Add(experience);
							}
						}
					}
					else
					{
						throw new Exception("can not update doctor profile");
					}
					result = await userManager.UpdateAsync(Doctor);
					if (result.Succeeded)
					{
						foreach (var award in doctorUpdatess.Awards)
						{
							var awarddb = Doctor.doctor.awards.FirstOrDefault(e => e.Id == award.Id);
							if (awarddb != null)
							{
								awarddb.award = award.award;
								awarddb.year = award.year;
								
							}
							else
							{
								Doctor.doctor.awards.Add(award);
							}
						}
					}
					else
					{
						throw new Exception("can not update doctor profile");
					}
					result = await userManager.UpdateAsync(Doctor);
					if (result.Succeeded)
					{
						foreach (var membership in doctorUpdatess.Membership)
						{
							var membershipdb = Doctor.doctor.memberships.FirstOrDefault(e => e.Id == membership.Id);
							if (membershipdb != null)
							{
								membershipdb.description = membership.description;
								
							}
							else
							{
								Doctor.doctor.memberships.Add(membership);
							}
						}
					}
					else
					{
						throw new Exception("can not update doctor profile");
					}
					result = await userManager.UpdateAsync(Doctor);
					if (result.Succeeded)
					{
						var clinicdb = Doctor.doctor.clinics.FirstOrDefault(e => e.Id == doctorUpdatess.Clinic.Id);
						if (clinicdb != null)
						{
							clinicdb.Address = doctorUpdatess.Clinic.Address;
							clinicdb.Name=doctorUpdatess.Clinic.Name;
							clinicdb.FromDay = doctorUpdatess.Clinic.FromDay;
							clinicdb.ToDay = doctorUpdatess.Clinic.ToDay;
							clinicdb.FromTime = doctorUpdatess.Clinic.FromTime;
							clinicdb.ToTime = doctorUpdatess.Clinic.ToTime;
						}
						else
						{
							Doctor.doctor.clinics.Add(doctorUpdatess.Clinic);
						}
					}
					else
					{
						throw new Exception("can not update doctor profile");
					}
					result = await userManager.UpdateAsync(Doctor);
					if (result.Succeeded)
					{

						scope.Complete();	
					}
					else
					{
						throw new Exception("can not update doctor profile");
					}
				}
				catch (Exception ex)
				{
					Doctor = null;
					scope.Dispose();
					
				}
			}
			return Doctor;

		}
	}
}
