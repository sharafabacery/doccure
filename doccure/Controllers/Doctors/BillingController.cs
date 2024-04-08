using doccure.Data.RequestModels;
using doccure.Repositories.Implementaion;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace doccure.Controllers.Doctors
{
	public class BillingController : Controller
	{
		private readonly IBillingService billingService;

		public BillingController(IBillingService billingService)
		{
			this.billingService = billingService;
		}
		[Authorize(Roles = "doctor")]
		public async Task<IActionResult> AddBills(BillingRequest billingRequest)
		{
			var Prescrptions = await billingService.AddEditBilling(billingRequest);
			if (Prescrptions == null)
			{
				//adding alernative path for nulls
				return View();
			}
			else
			{

				return RedirectToAction("Index", "PatientAppiontmentProfileDoctorView", new { id = Prescrptions.patientId });
			}

		}
		public async Task<IActionResult> ViewBills(int BookingId, string PatientId, string state)
		{
			var Prescrptions = await billingService.GetBillingByBookingId(BookingId);
			if (Prescrptions == null)
			{
				return RedirectToAction("Index", "PatientAppiontmentProfileDoctorView", new { id = PatientId });
			}
			else
			{
				ViewBag.State = state;
				ViewBag.lastBilling = Prescrptions;
				return View("Index");
			}

		}
		[Authorize(Roles = "doctor")]
		[Route("/Billing/DeleteBilling/{BillingID}")]

		[HttpDelete]
		public async Task<IActionResult> DeleteBilling(int BillingID)
		{
			var Prescrptions = await billingService.DeleteBilling(BillingID, User);
			if (Prescrptions == true)
			{
				return Ok();
			}
			else
			{
				return NotFound();
			}

		}
		[Authorize(Roles = "doctor")]
		[Route("/Billing/DeleteBookingBilling/{BookingId}")]

		[HttpDelete]
		public async Task<IActionResult> DeleteBookingBilling(int BookingId)
		{
			var Prescrptions = await billingService.DeleteAllBillingByBookingId(BookingId);
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

