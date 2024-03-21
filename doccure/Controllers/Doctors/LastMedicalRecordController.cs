using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace doccure.Controllers.Doctors
{
	public class LastMedicalRecordController : Controller
	{
		private readonly ILastMedicalRecordBookingPatientService lastMedicalRecordBookingPatientService;

		public LastMedicalRecordController(ILastMedicalRecordBookingPatientService lastMedicalRecordBookingPatientService)
		{
			this.lastMedicalRecordBookingPatientService = lastMedicalRecordBookingPatientService;
		}
		[Authorize(Roles = "doctor")]

		public async Task<IActionResult> Index(string Id)
		{
			var lastMidical = await lastMedicalRecordBookingPatientService.LastMedicalRecord(User, Id);
			if (lastMidical == null)
			{
				return NotFound();
			}
			else
			{
				return Ok(lastMidical);
			}
		}
	}
}
