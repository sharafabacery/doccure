﻿using doccure.Data.RequestModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace doccure.Controllers.patient
{
	public class DoctorSearchController : Controller
	{
		private readonly IDoctorSearch doctorSearch;

		public DoctorSearchController(IDoctorSearch doctorSearch)
		{
			this.doctorSearch = doctorSearch;
		}
		public async Task<IActionResult> IndexAsync(DoctorSearchBarRequest doctorSearchBar)
		{
			var result=await doctorSearch.SearchDoctors(doctorSearchBar);
			ViewBag.Doctors = result.doctorSearchReturneds;
			ViewBag.NumberOfDoctors=result.doctorSearchReturneds.Count;
			ViewBag.ClinicImages = result.clinicImages;
			return View("DoctorSearch");
		}
		[ActionName("DoctorSearches")]
		public async Task<IActionResult> IndexAsync(DoctorSearch doctorsSearch)
		{
			var result = await doctorSearch.SearchDoctors(doctorsSearch);
			ViewBag.Doctors = result.doctorSearchReturneds;
			ViewBag.NumberOfDoctors = result.doctorSearchReturneds.Count;
			ViewBag.ClinicImages = result.clinicImages;
			return View("DoctorSearch");
		}
		public async Task<IActionResult> GetDoctor(string Id)
		{
			var Doctor=await doctorSearch.GetDoctorData(Id);
			if (Doctor.applicationuser == null)
			{
				//Not Found Page
				return View("DoctorSearch");
			}
			ViewBag.Doctor = Doctor; 
			return View("DoctorProfile");

		}
	}
}
