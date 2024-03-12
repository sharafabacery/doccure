using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace doccure.Controllers.Doctors
{
	public class LastAppoitmentController : Controller
	{
		private readonly ILastAppointmentofPatient lastAppointmentofPatient;

		public LastAppoitmentController(ILastAppointmentofPatient lastAppointmentofPatient)
		{
			this.lastAppointmentofPatient = lastAppointmentofPatient;
		}
		[Authorize(Roles = "doctor")]
		public async Task<IActionResult> Index(string Id)
		{
			var lastAppiontment = await lastAppointmentofPatient.LastAppoitment(User, Id);
			if (lastAppiontment == null)
			{
				return RedirectToAction("Index", "PatientAppiontmentProfileDoctorView", new { id = Id });
			}
			else
			{
				ViewBag.lastAppiontment = lastAppiontment;
				return View("../Prescription/Index");
			}
		}
	}
}
