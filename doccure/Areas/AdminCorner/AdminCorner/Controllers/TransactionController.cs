using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace doccure.Areas.AdminCorner.AdminCorner.Controllers
{
	[Area("AdminCorner")]
	[Authorize(Roles = "admin")]
	public class TransactionController : Controller
	{
		private readonly ITransactionsListService transactionsListService;

		public TransactionController(ITransactionsListService transactionsListService)
		{
			this.transactionsListService = transactionsListService;
		}
		public async Task<IActionResult> Index()
		{
			var Transactions =await transactionsListService.GetAllTransactionss();
			ViewBag.Transactions = Transactions;
			return View();
		}
	}
}
