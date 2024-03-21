using doccure.Data.RequestModels;
using doccure.Repositories.Implementaion;
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
		[Authorize(Roles = "doctor")]
		public async Task<IActionResult> AddMedicalRecord(MedicalRecordRequest medicalRecordRequest)
		{
			var MedicalRecord = await medicalRecordService.AddEditMedicalRecord(medicalRecordRequest);
			if (MedicalRecord == null)
			{
				//adding alernative path for nulls
				return RedirectToAction("Index", "PatientAppiontmentProfileDoctorView", new { id = medicalRecordRequest.PatientId });
			}
			else
			{

				return RedirectToAction("Index", "PatientAppiontmentProfileDoctorView", new { id = MedicalRecord.patientId });
			}

		}
		[Authorize(Roles = "doctor")]
		[Route("/MedicalRecord/DeleteMedicalRecord/{MedicalRecordId}")]

		[HttpDelete]
		public async Task<IActionResult> DeleteMedicalRecord(int MedicalRecordId)
		{
			var MedicalRecorddb = await medicalRecordService.DeleteMedicalRecord(MedicalRecordId, User);
			if (MedicalRecorddb == true)
			{
				return Ok();
			}
			else
			{
				return NotFound();
			}

		}
		[Route("/MedicalRecord/GetMedicalRecord/{BookingId}")]
		public async Task<IActionResult> GetMedicalRecord(int BookingId)
		{
			var MedicalRecord = await medicalRecordService.GetMedicalRecordByBookingId(BookingId);
			if (MedicalRecord == null)
			{
				return NotFound();
			}
			else
			{
				return Ok(MedicalRecord);
			}

		}
		//public IActionResult Index()
		//{
		//	return View();
		//}
	}
}
