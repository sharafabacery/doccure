using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace doccure.Controllers.Doctors
{
	[Authorize(Roles ="doctor")]
	public class DeleteEducationController : Controller
	{
		private readonly IDeleteEducation deleteEducation;

		public DeleteEducationController(IDeleteEducation deleteEducation) {
			this.deleteEducation = deleteEducation;
		}
		[HttpDelete]
		public async Task<IActionResult> DeleteEducation(int id)
		{

			var DeletedEducation =await deleteEducation.DeleteEducationAsync(id, User);
			if (DeletedEducation == true)
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
