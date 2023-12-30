using doccure.Repositories.Interfance;

namespace doccure.Repositories.Implementaion
{
	public class ImageService: IFileService
	{
		IWebHostEnvironment webHostEnvironment;

		public ImageService(IWebHostEnvironment webHostEnvironment)
		{
			this.webHostEnvironment = webHostEnvironment;
		}
		public string SaveFile(IFormFile formFile)
		{ 
			try
			{
				var wwwpath = this.webHostEnvironment.WebRootPath;
				var path = Path.Combine(wwwpath, "img/uploads");
				if (Directory.Exists(path)==false)
				{
					Directory.CreateDirectory(path);
				}
				var ext = Path.GetExtension(formFile.FileName);
				var allowedExtension = new string[] { ".jpg", ".png", ".jpeg" };
				if (!allowedExtension.Contains(ext))
				{
					//string msg=string.Format()
					return "";
				}
				else
				{
					string uniquestring = Guid.NewGuid().ToString();
					var newFileName = uniquestring + ext;
					var filewithpath = Path.Combine(path, newFileName);
					var stream = new FileStream(filewithpath, FileMode.Create);
					formFile.CopyTo(stream);
					stream.Close();
					return newFileName;
				}
			}
			catch (Exception ex)
			{
				return "";
			}
		}
		public bool DeleteFile(string imageFile)
		{
			try
			{
				var wwwpath = this.webHostEnvironment.WebRootPath;
				var path = Path.Combine(wwwpath, "uploads\\", imageFile);
				if (System.IO.File.Exists(path))
				{
					System.IO.File.Delete(path);
					return true;
				}
				return false;

			}
			catch (Exception ex) { return false; }
		}
	}
}
