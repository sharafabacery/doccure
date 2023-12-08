using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Mvc;

namespace doccure.Controllers.Doctors
{
	public class DeleteMembershipController : Controller
	{
		private readonly IDeleteMembershipService deleteMembershipService;

		public DeleteMembershipController(IDeleteMembershipService deleteMembershipService)
		{
			this.deleteMembershipService = deleteMembershipService;
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteMembership(int id)
		{

			var DeletedMembership = await deleteMembershipService.DeleteMembershipAsync(id, User);
			if (DeletedMembership == true)
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
