using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace doccure.Controllers.Doctors
{
	[Authorize()]
	public class MedicalRecordController : Controller
	{
		private readonly IMedicalRecordService medicalRecordService;

		public MedicalRecordController(IMedicalRecordService medicalRecordService)
		{
			this.medicalRecordService = medicalRecordService;
		}
		public IActionResult Index()
		{
			return View();
		}
	}
}
