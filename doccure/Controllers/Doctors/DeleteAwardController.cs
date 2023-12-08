using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace doccure.Controllers.Doctors
{
	[Authorize(Roles = "doctor")]
	public class DeleteAwardController : Controller
	{
		private readonly IDeleteAwardService awardService;

		public DeleteAwardController(IDeleteAwardService awardService)
		{
			this.awardService = awardService;
		}
		[HttpDelete]
		public async Task<IActionResult> DeleteAward(int id)
		{

			var DeletedAward= await awardService.DeleteAwardAsync(id, User);
			if (DeletedAward == true)
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
