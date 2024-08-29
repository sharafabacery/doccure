using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Mvc;

namespace doccure.Controllers
{
	public class VideoController : Controller
	{
		private readonly IGetCallerInfoService getCallerInfoService;
		public VideoController(IGetCallerInfoService getCallerInfoService) {
			this.getCallerInfoService = getCallerInfoService;
		}
		public async Task<IActionResult> Index(string meetingId, string type)
		{
			var callerInfo = await getCallerInfoService.GetCallerInfo(User, meetingId);
			var userInfo = await getCallerInfoService.GetUser(User);
			ViewBag.meetingId = meetingId;
			ViewBag.type = type;
			ViewBag.callerInfo = callerInfo;
			ViewBag.userInfo = userInfo;
			return View();
		}
	}
}
