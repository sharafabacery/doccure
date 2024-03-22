using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Mvc;

namespace doccure.Controllers.Doctors
{
	public class BillingController : Controller
	{
		private readonly IBillingService billingService;

		public BillingController(IBillingService billingService)
		{
			this.billingService = billingService;
		}
		public IActionResult Index()
		{
			return View();
		}
	}
}
