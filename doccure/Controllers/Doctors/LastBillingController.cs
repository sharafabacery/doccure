using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace doccure.Controllers.Doctors
{
	public class LastBillingController : Controller
	{
		private readonly ILastBillingService lastBillingService;

		public LastBillingController(ILastBillingService lastBillingService)
		{
			this.lastBillingService = lastBillingService;
		}
		[Authorize(Roles = "doctor")]
		public async Task<IActionResult> Index(string Id)
		
		{
			var lastBilling = await lastBillingService.LastBilling(User, Id);
			if (lastBilling == null)
			{
				return RedirectToAction("Index", "PatientAppiontmentProfileDoctorView", new { id = Id });
			}
			else
			{
				ViewBag.lastBilling = lastBilling;
				return View("../Billing/Index");
			}
		}
	}
}
