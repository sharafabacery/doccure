using doccure.Data;
using doccure.Data.Models;
using doccure.Data.RequestModels;
using doccure.Data.ResponseModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Elfie.Serialization;
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
			message.Message = messageRequest.Message;
			message.File = messageRequest.UploadedFile;
			await applicationDbContext.Messages.AddAsync(message);
			var res = await applicationDbContext.SaveChangesAsync();
			if (res > 0) return message;
			else return null;
			
		}

		public async Task<List<MessageDTOResponse>> GetMessages(MessageQueryRequest messageQuery)
		{	
			var minDate = await applicationDbContext.Messages.Where(e => ((e.senderId == messageQuery.Sender && e.receiverId == messageQuery.Reciver) || (e.senderId == messageQuery.Reciver && e.receiverId == messageQuery.Sender))).MinAsync(e=>e.CreatedTime);
			if (minDate.Year == 0001||(messageQuery.Date.Value.CompareTo(minDate)<1)) { return null; }
			var messages = await applicationDbContext.Messages.Where(e=>(((e.senderId==messageQuery.Sender &&e.receiverId==messageQuery.Reciver)||(e.senderId == messageQuery.Reciver && e.receiverId == messageQuery.Sender))&&e.CreatedTime.Year==messageQuery.Date.Value.Year && e.CreatedTime.Month == messageQuery.Date.Value.Month && e.CreatedTime.Day == messageQuery.Date.Value.Day)).OrderByDescending(e=>e.Read).OrderByDescending(e=>e.CreatedTime)
				.Select(cc=>  new MessageDTOResponse() { Id =cc.Id, Message=cc.Message, CreatedTime=cc.CreatedTime.ToString(), Read=cc.Read, SoftDelete=cc.SoftDelete, File=cc.File,receiverId=cc.receiverId,senderId=cc.senderId })
				.ToListAsync();
			return messages;
		}

		public async Task<bool> MarkReadMessages(MessageQueryRequest messageQuery)
		{
			var res = await applicationDbContext.Messages
							.Where(e => e.senderId == messageQuery.Sender && e.receiverId == messageQuery.Reciver && e.CreatedTime.Year == messageQuery.Date.Value.Year && e.CreatedTime.Month == messageQuery.Date.Value.Month && e.CreatedTime.Day == messageQuery.Date.Value.Day)
							.ExecuteUpdateAsync(e => e.SetProperty(s => s.Read, true));
			if (res > 0) return true;
			else return false;
		}

		public async Task<bool> UpdateMessage(MessageUser messageUser)
		{
			var user = await applicationDbContext.UserGroups.Where(e => e.applicationuserId == messageUser.Reciver && e.Active == true).FirstOrDefaultAsync();
			if (user == null) return false;
			var res = await applicationDbContext.Messages
							.Where(e => e.senderId == messageUser.Sender && e.receiverId == messageUser.Reciver && e.Id==messageUser.MessageId)
							.ExecuteUpdateAsync(e => e.SetProperty(s => s.Read, true));
			if (res > 0) return true;
			else return false;
		}
	}
}
