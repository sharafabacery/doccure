using doccure.Data;
using doccure.Data.Models;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace doccure.Repositories.Implementaion
{
	public class DoctorDTO
	{
		public string Id { get; set; }
		public string FullName { get; set; }
		public string Image { get; set; }
		public string Speciality { get; set; }
		public int Earned { get; set; }
	}
	public class DoctorListDashboardService : IDoctorListDashboardService
	{
		private readonly UserManager<Applicationuser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly ApplicationDbContext applicationDbContext;

		public DoctorListDashboardService(UserManager<Applicationuser> userManager, RoleManager<IdentityRole> roleManager,ApplicationDbContext applicationDbContext)
		{
			this.userManager = userManager;
			this.roleManager = roleManager;
			this.applicationDbContext = applicationDbContext;
		}
		public async Task<List<DoctorDTO>> GetAllUsers()
		{
			
					var users = await userManager.Users
			.Include(u => u.address) // Include the Department navigation property
			.Include(u => u.DoctorBooking) // Include the Department navigation property
			.Include(u => u.doctor.Speciality) // Include the Department navigation property
			.Where(u =>u.doctor!=null) // Filter users with the "employee" role

			.Select(e=>new DoctorDTO
			{
				Id=e.Id,
				FullName=e.FirstName+' '+e.LastName,
				Speciality=e.doctor.Speciality.Name,
				Earned=(int)e.DoctorBooking.Sum(c=>c.total),
				Image=e.Image
			})
			.ToListAsync(); 
			return users;
		}
	}
}
