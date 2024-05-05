using doccure.Data.Models;
using doccure.Data;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace doccure.Repositories.Implementaion
{
	public class PatientDTO
	{
		public String PatientId { get; set; }
		public String PatientName { get; set;}
		public int? Age { get; set; }
		public string? Address { get; set; }
		public string? Phone { get; set; }
		public DateTime? LastVisit { get; set; }
		public double? Paid { get; set; }
	}
	public class PatientListService : IPatientListService
	{
		private readonly UserManager<Applicationuser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly ApplicationDbContext applicationDbContext;

		public PatientListService(UserManager<Applicationuser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext applicationDbContext)
		{
			this.userManager = userManager;
			this.roleManager = roleManager;
			this.applicationDbContext = applicationDbContext;
		}
		public async Task<List<PatientDTO>> GetAllUsers(bool pagination)
		{
			var adminUserIds = await userManager.GetUsersInRoleAsync("admin");
			List<PatientDTO> userslist;
			IQueryable<PatientDTO> users =  applicationDbContext.Users
			.Include(u => u.address) // Include the Department navigation property
			.Include(u => u.doctor) // Include the Department navigation property
			.Include(u => u.PatientBooking) // Include the Department navigation property

			.Where(u => u.doctor == null && adminUserIds.Contains(u) == false) // Filter users with the "employee" role

			.Select(e =>

				new PatientDTO
				{
					PatientId = e.Id.Substring(0,5),
					PatientName = e.FirstName + ' ' + e.LastName,
					Address = e.address == null ? null : e.address.Address1,
					Phone = e.PhoneNumber,
					Age = e.DateofBirth!=null? DateTime.Today.Year - e.DateofBirth.Value.Year:null,
					LastVisit = e.PatientBooking.FirstOrDefault(c => c.Id == e.PatientBooking.Max(e => e.Id)) == null ? null : e.PatientBooking.FirstOrDefault(c => c.Id == e.PatientBooking.Max(e => e.Id)).bookingDate,
					Paid = e.PatientBooking.FirstOrDefault(c => c.Id == e.PatientBooking.Max(e => e.Id)) == null ? null : e.PatientBooking.FirstOrDefault(c => c.Id == e.PatientBooking.Max(e => e.Id)).total
				}

			);
			if (pagination) users.Take(5);
			userslist = await users.ToListAsync();
			return userslist;
		}
	}
}
