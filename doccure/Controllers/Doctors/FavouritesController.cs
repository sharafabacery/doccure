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
			var favourites = await favouritesServcie.GetFavouriteDoctorsByPatientId(User);
			ViewBag.Favourites = favourites;
			return View();
		}

		public async Task<IActionResult> CreateFavourite(int DoctorId)
		{
			var createdFavourite = await favouritesServcie.CreateFavouriteDoctor(User, DoctorId);
			if (createdFavourite) return Ok();
			else
			{
				return NotFound();
			}
			
		}
		[HttpDelete]
		public async Task<IActionResult> DeleteFavourite(int id)
		{

			var DeletedFavourite = await favouritesServcie.DeleteFavouriteDoctor( User,id);
			if (DeletedFavourite == true)
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
