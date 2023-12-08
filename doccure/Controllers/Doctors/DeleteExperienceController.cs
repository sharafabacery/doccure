using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace doccure.Controllers.Doctors
{
	[Authorize(Roles = "doctor")]
	public class DeleteExperienceController : Controller
	{
		private readonly IDeleteExperience deleteExperience;

		public DeleteExperienceController(IDeleteExperience deleteExperience)
		{
			this.deleteExperience = deleteExperience;
		}
		[HttpDelete]
		public async Task<IActionResult> DeleteExperience(int id)
		{

			var DeleteExperien = await deleteExperience.DeleteExperienceAsync(id, User);
			if (DeleteExperien == true)
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
