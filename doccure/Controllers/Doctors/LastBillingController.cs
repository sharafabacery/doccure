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
			return View();
		}
	}
}
