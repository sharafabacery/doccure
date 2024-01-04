﻿using doccure.Data;
using doccure.Data.RequestModels;
using doccure.Data.ResponseModels;
using doccure.Repositories.Interfance;
using Microsoft.EntityFrameworkCore;

namespace doccure.Repositories.Implementaion
{
	public class DoctorSearchService : IDoctorSearch
	{
		private readonly ApplicationDbContext applicationDbContext;

		public DoctorSearchService(ApplicationDbContext applicationDbContext)
		{
			this.applicationDbContext = applicationDbContext;
		}
		

		public  Task<Object> SearchDoctors(DoctorSearchBarRequest doctorSearchBarRequest)
		{
			int NumberOfRowsReturned = 10;
			int SkipRows = doctorSearchBarRequest.SkipRows == null ? 0 : (int)doctorSearchBarRequest.SkipRows;
			int LowerLimit = NumberOfRowsReturned * SkipRows;
			int UpperLimit = LowerLimit + NumberOfRowsReturned;
			applicationDbContext.Database.ExecuteSqlRaw($"CREATE VIEW DoctorSearchReturned AS SELECT * \r\nFROM(\r\nSELECT ROW_NUMBER() OVER ( ORDER BY U.Id ) AS RowNum,  U.Id, U.FirstName+' '+U.LastName AS FULLNAME,S.Name,S.image,D.Price,C.Address,D.Services\r\nFROM [dbo].[AspNetUsers] U\r\nJOIN [dbo].[AspNetUserRoles] UR\r\nON U.Id=UR.UserId\r\nJOIN [dbo].[AspNetRoles] R\r\nON UR.RoleId=R.Id AND R.Name='doctor'\r\nLEFT JOIN [dbo].[Doctor] D\r\nON U.Id=D.applicationuserId\r\nLEFT JOIN [dbo].[Clinics] C\r\nON C.DoctorId=D.Id\r\n--LEFT JOIN [dbo].[ClinicImages] CI\r\n--ON C.Id=CI.ClinicId\r\nJOIN [dbo].[Speciality] S\r\nON S.Id=D.SpecialityId\r\nWHERE C.Address LIKE '%{doctorSearchBarRequest.Location}%' AND ((U.FirstName+' '+U.LastName LIKE '%{doctorSearchBarRequest.SearchInput}%') OR C.Name LIKE '%{doctorSearchBarRequest.SearchInput}%' OR D.Specialization LIKE '%{doctorSearchBarRequest.SearchInput}%' OR D.Services LIKE '%{doctorSearchBarRequest.SearchInput}%' OR S.Name='%{doctorSearchBarRequest.SearchInput}%')\r\n) AS Result\r\nWHERE RowNum>={LowerLimit} AND RowNum<={UpperLimit}");
			var postCounts = applicationDbContext.DoctorSearchReturned.ToList();
			List<DoctorSearchReturned> doctorSearchReturneds = new List<DoctorSearchReturned>();
			return null;
		}
	}
}
