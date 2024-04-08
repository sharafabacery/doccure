using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace doccure.Controllers.Doctors
{
	[Authorize]
	public class FavouritesController : Controller
	{
		private readonly IFavouritesServcie favouritesServcie;

		public FavouritesController(IFavouritesServcie favouritesServcie) {
			this.favouritesServcie = favouritesServcie;
		}
		public async Task<IActionResult> Index()
		{
			return View();
		}

		public async Task<bool> CreateFavourite(string DoctorId)
		{
			return true;
		}
	}
}
