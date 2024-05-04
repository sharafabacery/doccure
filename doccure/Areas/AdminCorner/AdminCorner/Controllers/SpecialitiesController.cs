using doccure.Data.RequestModels;
using doccure.Repositories.Implementaion;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace doccure.Areas.AdminCorner.AdminCorner.Controllers
{
	[Area("AdminCorner")]
	[Authorize(Roles ="admin")]
	public class SpecialitiesController : Controller
	{
		private readonly ISpecalityService specalityService;

		public SpecialitiesController(ISpecalityService specalityService) {
			this.specalityService = specalityService;
		}

		public async Task< IActionResult> Index()
		{
			var Specialities =await specalityService.GetSpecialities();
			ViewBag.Specialities = Specialities;
			return View();
		}
		public async Task<IActionResult> AddSpeciality(SpecialityRequest specialityRequest)
		{
			var sp = await specalityService.AddSpeciality(specialityRequest);
			if (sp!=null){
				return RedirectToAction("Index", "Specialities");
			}else
			{
				TempData["error"] = "Error in Adding";
				return RedirectToAction("Index", "Specialities");
			}
		}
		public async Task<IActionResult> EditSpeciality(SpecialityRequest specialityRequest)
		{
			var sp = await specalityService.EditSpeciality(specialityRequest);
			if (sp != null)
			{
				return RedirectToAction("Index", "Specialities");
			}
			else
			{
				TempData["error"] = "Error in Edit";
				return RedirectToAction("Index", "Specialities");
			}
		}
		public async Task<IActionResult> DeleteSpeciality(int id)
		{
			var reviews = await specalityService.RemoveSpeciality(id);
			if (reviews)
			{
				return Ok();
			}
			else
			{
				return NotFound();
			}
		}
	}
}
