using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace doccure.Controllers.Doctors
{
	[Authorize(Roles ="doctor")]
	public class ClinicController : Controller
	{
		private readonly IClinicService clinicService;

		public ClinicController(IClinicService clinicService)
		{
			this.clinicService = clinicService;
		}
		public async Task<IActionResult> Index(int id)
		{
			var clinic = await clinicService.GetClinicById(id, User);
			if(clinic == null)
			{
				return NotFound();
			}
			else
			{
				return Ok(clinic);
			}
		}
	}
}
