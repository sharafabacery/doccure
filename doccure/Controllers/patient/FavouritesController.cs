using doccure.Data.RequestModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static doccure.Program;

namespace doccure.Controllers.patient
{
    [Authorize]
    public class FavouritesController : Controller
    {
        private readonly IFavouritesServcie favouritesServcie;
		private readonly IUserProfileSettingsService userProfileSettingsService;
		public FavouritesController(ServiceResolver serviceAccessor,IFavouritesServcie favouritesServcie)
        {
            this.favouritesServcie = favouritesServcie;
			this.userProfileSettingsService = serviceAccessor("Patient");

		}
        public async Task<IActionResult> Index()
        {
            var patient = await userProfileSettingsService.GetUserData(User); 
            var favourites = await favouritesServcie.GetFavouriteDoctorsByPatientId(User);
            ViewBag.Favourites = favourites;
            ViewBag.User = patient;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateFavourite([FromBody] DoctorFavouriteId DoctorId)
        {
            var createdFavourite = await favouritesServcie.CreateFavouriteDoctor(User, DoctorId.Id);
            if (createdFavourite) return Ok();
            else
            {
                return NotFound();
            }

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteFavourite(int id)
        {

            var DeletedFavourite = await favouritesServcie.DeleteFavouriteDoctor(User, id);
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
