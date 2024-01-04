using doccure.Data.RequestModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Mvc;

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
			return View();
		}
	}
}
