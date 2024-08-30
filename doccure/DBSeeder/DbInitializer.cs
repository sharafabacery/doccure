using doccure.Data;
using doccure.Data.Models;
using doccure.Data.RequestModels;
using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Identity;

namespace doccure.DBSeeder
{
	internal class DbInitializer
	{
		internal static async void Initialize(ApplicationDbContext applicationDbContext, UserManager<Applicationuser> userManager, RoleManager<IdentityRole> roleManager)
		{
			ArgumentNullException.ThrowIfNull(applicationDbContext, nameof(applicationDbContext));

			applicationDbContext.Database.EnsureCreated();
			if (applicationDbContext.Users.Any()) return;
			applicationDbContext.SaveChanges();
			
			List<Speciality> specialities = new List<Speciality>();
			specialities.Add(new Speciality { Name = "Urology",image= "specialities-01.png" });
			specialities.Add(new Speciality { Name = "Neurology", image= "specialities-02.png" });
			specialities.Add(new Speciality { Name = "Orthopedic", image= "specialities-03.png" });
			specialities.Add(new Speciality { Name = "Cardiologist2", image= "specialities-04.png" });
			specialities.Add(new Speciality { Name = "Dentist", image= "specialities-05.png" });
			applicationDbContext.Speciality.AddRange(specialities);
			applicationDbContext.SaveChanges();

			await  roleManager.CreateAsync(new IdentityRole("patient"));
			await  roleManager.CreateAsync(new IdentityRole("doctor"));
			await roleManager.CreateAsync(new IdentityRole("admin"));
			
			int adminSeeder = 1;
			var adminUser = SeederEntityConfigration.UsersGenrated(adminSeeder);
			foreach(var user in adminUser) {
				await userManager.CreateAsync(user);
				await userManager.AddToRoleAsync(user, "admin");
			}

			int normalUserSeeder = 10;
			var normalUsers = SeederEntityConfigration.UsersGenrated(normalUserSeeder);
			foreach (var user in normalUsers)
			{
				await userManager.CreateAsync(user);
				await userManager.AddToRoleAsync(user, "patient");
			}

			int doctorUserSeeder = 7;
			var doctors = SeederEntityConfigration.DoctorGenrated(doctorUserSeeder,specialities);
			foreach (var user in doctors)
			{
				await userManager.CreateAsync(user);
				await userManager.AddToRoleAsync(user, "doctor");
			}


		}
	}
}
