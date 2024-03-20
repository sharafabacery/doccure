using doccure.Repositories.Interfance;

namespace doccure.Repositories.Implementaion
{
	public class FileService : IFileService
	{
		IWebHostEnvironment webHostEnvironment;

		public FileService(IWebHostEnvironment webHostEnvironment)
		{
			this.webHostEnvironment = webHostEnvironment;
		}
		public string SaveFile(IFormFile formFile)
		{
			try
			{
				var wwwpath = this.webHostEnvironment.WebRootPath;
				var path = Path.Combine(wwwpath, "pdfs");
				if (Directory.Exists(path) == false)
				{
					Directory.CreateDirectory(path);
				}
				if (formFile == null) return "";
				var ext = Path.GetExtension(formFile.FileName);
				var allowedExtension = new string[] { ".pdf" };
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
				var path = Path.Combine(wwwpath, "pdfs\\", imageFile);
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
