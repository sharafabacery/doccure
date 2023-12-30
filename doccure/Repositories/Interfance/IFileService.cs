namespace doccure.Repositories.Interfance
{
	public interface IFileService
	{
		string SaveFile(IFormFile formFile);
		bool DeleteFile(string imageFile);
	}
}
