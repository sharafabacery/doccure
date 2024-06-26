﻿using doccure.Data;
using doccure.Data.Models;
using doccure.Data.RequestModels;
using doccure.Data.ResponseModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace doccure.Repositories.Implementaion
{
    public class DoctorSearchService : IDoctorSearch
	{
		private readonly ApplicationDbContext applicationDbContext;
		private readonly UserManager<Applicationuser> userManager;

		public DoctorSearchService(ApplicationDbContext applicationDbContext, UserManager<Applicationuser> userManager)
		{
			this.applicationDbContext = applicationDbContext;
			this.userManager = userManager;
		}

		public async Task<DoctorData> GetDoctorData(string Id)
		{
			var Doctor = await userManager.Users.Include(a => a.address).Include(a => a.doctor).Include(a => a.doctor.educations).Include(a => a.doctor.experiences).Include(a => a.doctor.awards).Include(a => a.doctor.memberships).Include(a => a.doctor.clinics).FirstOrDefaultAsync(usr => usr.Id == Id);
			List<ClinicImage>? ClinicImages = null;
			if (Doctor != null)
			{
				var ClinicIds = Doctor.doctor.clinics?.Select(c => c.Id).ToList();
				if (ClinicIds != null)
				{
					ClinicImages = await applicationDbContext.ClinicImages.Where(s => ClinicIds.Contains(s.ClinicId)).ToListAsync();
				}
				
			}

			return new DoctorData { applicationuser= Doctor , clinicImages= ClinicImages };
		}

		public async Task<Doctor> GetDoctorDataByDoctorId(int Id)
		{
			var Doctor = await applicationDbContext.Doctor.Include(d => d.applicationuser).FirstOrDefaultAsync(v => v.Id == Id);
			if (Doctor == null) return null;
			return Doctor;

		}

		public async Task<DoctorsSearch> SearchDoctors(DoctorSearchBarRequest doctorSearchBarRequest)
		{
			int NumberOfRowsReturned = 10;
			int SkipRows = doctorSearchBarRequest.SkipRows == null ? 0 : (int)doctorSearchBarRequest.SkipRows;
			int LowerLimit = NumberOfRowsReturned * SkipRows;
			int UpperLimit = LowerLimit + NumberOfRowsReturned;
			
			var DoctorSearchReturneds =await applicationDbContext
				.DoctorSearchReturned
				.FromSqlRaw($"SELECT * \r\nFROM(\r\nSELECT ROW_NUMBER() OVER ( ORDER BY U.Id ) AS RowNum,  U.Id, U.FirstName+' '+U.LastName AS FULLNAME,S.Name,S.Name AS SPECALITYNAME,U.image AS PROFILEIMAGE,S.image,D.Price,C.Address,C.Id AS ClinicID,D.Specialization,D.Services\r\nFROM [dbo].[AspNetUsers] U\r\nJOIN [dbo].[AspNetUserRoles] UR\r\nON U.Id=UR.UserId\r\nJOIN [dbo].[AspNetRoles] R\r\nON UR.RoleId=R.Id AND R.Name='doctor'\r\nLEFT JOIN [dbo].[Doctor] D\r\nON U.Id=D.applicationuserId\r\nLEFT JOIN [dbo].[Clinics] C\r\nON C.DoctorId=D.Id\r\n--LEFT JOIN [dbo].[ClinicImages] CI\r\n--ON C.Id=CI.ClinicId\r\nJOIN [dbo].[Speciality] S\r\nON S.Id=D.SpecialityId\r\nWHERE C.Address LIKE '%{doctorSearchBarRequest.Location}%' AND ((U.FirstName+' '+U.LastName LIKE '%{doctorSearchBarRequest.SearchInput}%') OR C.Name LIKE '%{doctorSearchBarRequest.SearchInput}%' OR D.Specialization LIKE '%{doctorSearchBarRequest.SearchInput}%' OR D.Services LIKE '%{doctorSearchBarRequest.SearchInput}%' OR S.Name='%{doctorSearchBarRequest.SearchInput}%')\r\n) AS Result\r\nWHERE RowNum>={LowerLimit} AND RowNum<={UpperLimit}")
				.ToListAsync();

			var ClinicIds = DoctorSearchReturneds.Select(c => c.ClinicID).ToList();
			var ClinicImages =await applicationDbContext.ClinicImages.Where(s=>ClinicIds.Contains(s.ClinicId)).ToListAsync();

			return new DoctorsSearch() {
				doctorSearchReturneds= DoctorSearchReturneds,
				clinicImages= ClinicImages
			};
		}

		public async Task<DoctorsSearch> SearchDoctors(DoctorSearch DoctorSearchRequest)
		{
			string Where = "";
			string WhereGender = "";
			string WhereSpcialities = "";
			if (DoctorSearchRequest.Gender!=null&&DoctorSearchRequest.Gender.Any())
			{
				Where = " WHERE ";
				WhereGender = " U.Gender IN(";
				for (int i = 0; i < DoctorSearchRequest.Gender.Count; i++)
				{
					WhereGender += DoctorSearchRequest.Gender[i];
					if (DoctorSearchRequest.Gender.Count > 1 && i != DoctorSearchRequest.Gender.Count - 1)
					{
						WhereGender += ",";
					}
				}
				WhereGender += ") ";
			}
			if (DoctorSearchRequest.Spcialites!=null&&DoctorSearchRequest.Spcialites.Any())
			{
				if (Where.Length == 0)
				{
					Where = " WHERE ";
				}
				WhereSpcialities = " S.Id IN(";
				for (int i = 0; i < DoctorSearchRequest.Spcialites.Count; i++)
				{
					WhereSpcialities += DoctorSearchRequest.Spcialites[i];
					if (DoctorSearchRequest.Spcialites.Count > 1 && i != DoctorSearchRequest.Spcialites.Count - 1)
					{
						WhereSpcialities += ",";
					}
				}
				WhereSpcialities += ") ";
			}
			if (WhereGender != "")
			{
				Where = Where + WhereGender;
				if (WhereSpcialities != "")
				{
					Where = Where + " AND " + WhereSpcialities+" ";
				}
			}
			else if (WhereSpcialities != "")
			{
				Where = Where + WhereSpcialities+" ";
			}

			int NumberOfRowsReturned = 10;
			int SkipRows = DoctorSearchRequest.SkipRows == null ? 0 : (int)DoctorSearchRequest.SkipRows;
			int LowerLimit = NumberOfRowsReturned * SkipRows;
			int UpperLimit = LowerLimit + NumberOfRowsReturned;
			var DoctorSearchReturneds = await applicationDbContext
				.DoctorSearchReturned
				.FromSqlRaw($"SELECT * \r\nFROM(\r\nSELECT ROW_NUMBER() OVER ( ORDER BY U.Id ) AS RowNum,  U.Id, U.FirstName+' '+U.LastName AS FULLNAME,S.Name,S.Name AS SPECALITYNAME,U.image AS PROFILEIMAGE,S.image,D.Price,C.Address,C.Id AS ClinicID,D.Specialization,D.Services\r\nFROM [dbo].[AspNetUsers] U\r\nJOIN [dbo].[AspNetUserRoles] UR\r\nON U.Id=UR.UserId\r\nJOIN [dbo].[AspNetRoles] R\r\nON UR.RoleId=R.Id AND R.Name='doctor'\r\nLEFT JOIN [dbo].[Doctor] D\r\nON U.Id=D.applicationuserId\r\nLEFT JOIN [dbo].[Clinics] C\r\nON C.DoctorId=D.Id\r\nJOIN [dbo].[Speciality] S\r\nON S.Id=D.SpecialityId {Where} \r\n\r\n)AS Result\r\nWHERE RowNum>=${LowerLimit} AND RowNum<=${UpperLimit}")
				.ToListAsync();

			var ClinicIds = DoctorSearchReturneds.Select(c => c.ClinicID).ToList();
			var ClinicImages = await applicationDbContext.ClinicImages.Where(s => ClinicIds.Contains(s.ClinicId)).ToListAsync();

			return new DoctorsSearch()
			{
				doctorSearchReturneds = DoctorSearchReturneds,
				clinicImages = ClinicImages
			};

			//throw new NotImplementedException();
		}
	}
}
