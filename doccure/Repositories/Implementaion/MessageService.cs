using doccure.Data;
using doccure.Data.Models;
using doccure.Data.RequestModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static doccure.Program;

namespace doccure.Repositories.Implementaion
{
	public class MessageService : IMessageService
	{
		private readonly ApplicationDbContext applicationDbContext;
		private readonly UserManager<Applicationuser> userManager;
		//private readonly IFileService ImageService;
		//private readonly IFileService fileService;

		public MessageService(ApplicationDbContext applicationDbContext, UserManager<Applicationuser> userManager)
		{
			this.applicationDbContext = applicationDbContext;
			this.userManager = userManager;
			//this.ImageService = serviceAccessorImage("Image");
			//this.fileService = serviceAccessorFile("File");
		}
		public async Task<Messages> AddMessage(MessageRequest messageRequest, string sender)
		{
			string? filePath = null;
			var message = new Messages();
			message.senderId = sender;
			message.receiverId = messageRequest.Reciver;
			message.CreatedTime = DateTime.Now;
			message.Read = false;
			message.SoftDelete = false;
			
			message.File = messageRequest.UploadedFile;
			await applicationDbContext.Messages.AddAsync(message);
			var res = await applicationDbContext.SaveChangesAsync();
			if (res > 0) return message;
			else return null;
			
		}

		public async Task<List<Messages>> GetMessages(MessageQueryRequest messageQuery)
		{
			var messages = await applicationDbContext.Messages.Include(e=>e.Receiver).Include(e=>e.Sender).Where(e=>((e.senderId==messageQuery.Sender &&e.receiverId==messageQuery.Reciver)||(e.senderId == messageQuery.Reciver && e.receiverId == messageQuery.Sender)&&e.CreatedTime.Year==messageQuery.date.Value.Year && e.CreatedTime.Month == messageQuery.date.Value.Month && e.CreatedTime.Day == messageQuery.date.Value.Day)).OrderByDescending(e=>e.Read).OrderByDescending(e=>e.CreatedTime).ToListAsync();
			return messages;
		}

		public async Task<bool> UpdateMessage(MessageUser messageUser)
		{
			var res = await applicationDbContext.Messages
							.Where(e => e.senderId == messageUser.Sender && e.receiverId == messageUser.Reciver).ExecuteUpdateAsync(e => e.SetProperty(s => s.Read, true));
			if (res > 0) return true;
			else return false;
		}
	}
}
