using doccure.Data.RequestModels;
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
			ViewBag.Doctors = result;
			ViewBag.NumberOfDoctors=result.Count();
			return View("DoctorSearch");
		}
	}
}
