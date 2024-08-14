using doccure.Data.RequestModels;
using doccure.Repositories.Implementaion;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Mvc;
using static doccure.Program;

namespace doccure.Controllers
{
	public class ImageUploaderController : Controller
	{
		private readonly IFileService fileService;
		public ImageUploaderController(ServiceResolver2 serviceAccessor) { this.fileService = serviceAccessor("Image"); }
		[HttpPost]
		public IActionResult UploagImage(IFormFile ImageFile)
		{
			var filePath= fileService.SaveFile(ImageFile);
		if(filePath != null)
			{
				return Ok( new  { path=filePath } );
			}
			else
			{
				return NotFound();
			}
		}
	}
}
