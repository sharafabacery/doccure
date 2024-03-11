using doccure.Data.RequestModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace doccure.Controllers.Doctors
{
	[Authorize()]

	public class PrescriptionController : Controller
	{
		private readonly ILastAppointmentofPatient lastAppointmentofPatient;
		private readonly IPrescriptionService prescriptionService;

		public PrescriptionController(ILastAppointmentofPatient lastAppointmentofPatient, IPrescriptionService prescriptionService)
		{
			this.lastAppointmentofPatient = lastAppointmentofPatient;
			this.prescriptionService = prescriptionService;

		}
		[Authorize(Roles = "doctor")]

		public async Task<IActionResult> GetLastAppoitment(string Id)
		{
			var lastAppiontment = await lastAppointmentofPatient.LastAppoitment(User, Id);
			if(lastAppiontment == null)
			{
				return RedirectToAction("Index", "PatientAppiontmentProfileDoctorView",new {id=Id });
			}
			else
			{
			ViewBag.lastAppiontment = lastAppiontment;
			return View("Index");
			}
			
		}
		public async Task<IActionResult> AddPrescription(PrescriptionRequest prescriptionRequest)
		{
			var Prescrptions=await prescriptionService.AddEditPrescription(prescriptionRequest);
			if(Prescrptions == null)
			{
				//adding alernative path for nulls
				return View();
			}
			else
			{

				return RedirectToAction("Index", "PatientAppiontmentProfileDoctorView", new { id = Prescrptions.patientId });
			}
			
		}
		public async Task<IActionResult> ViewPrescription(int BookingId,string PatientId,string state)
		{
			var Prescrptions = await prescriptionService.GetPrescriptionsByBookingId(BookingId);
			if (Prescrptions == null)
			{
				return RedirectToAction("Index", "PatientAppiontmentProfileDoctorView", new { id = PatientId });
			}
			else
			{
				ViewBag.State = state;
				ViewBag.lastAppiontment = Prescrptions;
				return View("Index");
			}
			
		}
        [Authorize(Roles = "doctor")]
        [Route("/Prescription/DeletePrescription/{PrescriptionID}")]

        [HttpDelete]
        public async Task<IActionResult> DeletePrescription(int PrescriptionID)
        {
            var Prescrptions = await prescriptionService.DeletePrescription(PrescriptionID, User);
            if (Prescrptions == true)
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
