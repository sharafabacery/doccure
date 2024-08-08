using doccure.Data;
using doccure.Data.Models;
using doccure.Data.RequestModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using static doccure.Program;

namespace doccure.Repositories.Implementaion
{
	public class MessageService : IMessageService
	{
		private readonly ApplicationDbContext applicationDbContext;
		private readonly UserManager<Applicationuser> userManager;
		private readonly IFileService ImageService;
		private readonly IFileService fileService;

		public MessageService(ApplicationDbContext applicationDbContext, UserManager<Applicationuser> userManager, ServiceResolver2 serviceAccessorImage, ServiceResolver2 serviceAccessorFile)
		{
			this.applicationDbContext = applicationDbContext;
			this.userManager = userManager;
			this.ImageService = serviceAccessorImage("Image");
			this.fileService = serviceAccessorFile("File");
		}
		public Task<Messages> AddMessage(MessageRequest messageRequest, string sender)
		{
			throw new NotImplementedException();
		}

		public Task<List<Messages>> GetMessages(DateTime? date)
		{
			throw new NotImplementedException();
		}
	}
}
