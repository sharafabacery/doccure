using doccure.Data;
using doccure.Data.Models;
using doccure.Data.ResponseModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Security.Claims;

namespace doccure.Repositories.Implementaion
{
	public class PatientsService : IPatientsService
	{
		private readonly ApplicationDbContext applicationDbContext;
		private readonly UserManager<Applicationuser> userManager;

		public PatientsService(ApplicationDbContext applicationDbContext, UserManager<Applicationuser> userManager)
		{
			this.applicationDbContext = applicationDbContext;
			this.userManager = userManager;
		}
		public async Task<List<Patients>> GetPatientsByDoctorId(ClaimsPrincipal claimsPrincipal)
		{
			List<Patients> Patients = new List<Patients>();
			Patients = await applicationDbContext.Patients.FromSqlRaw($"SELECT DISTINCT U.Id,U.FirstName,U.LastName,U.PhoneNumber,U.BloodGroup,U.Image AS ProfilePicture,DATEDIFF(YEAR,U.DateofBirth,GETDATE())AS Age,A.Address1 AS Address ,CASE WHEN U.Gender=0 THEN 'MALE' ELSE  'FEMALE' END AS Gender\r\nFROM [dbo].[AspNetUsers] U\r\nLEFT JOIN [dbo].[Address] A\r\nON U.Id=A.applicationuserId\r\nJOIN [dbo].[Bookings] B\r\nON U.Id=B.patientId\r\nWHERE B.doctorId='{userManager.GetUserId(claimsPrincipal)}'").ToListAsync();
			return Patients;
			
		}
	}
}
