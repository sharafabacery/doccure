
namespace doccure.Hub
{
	using doccure.Repositories.Interfance;
	using Google.Apis.Drive.v3.Data;
	using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Threading.Tasks;
	public class ChatHub : Hub
	{
		private readonly IChatService chatService;

		public ChatHub(IChatService chatService)
		{
			this.chatService = chatService;
		}
		public async Task AddConnection()
		{
			var user=await chatService.Connect(new Data.Models.UserConnected {ConnectionID=Context.ConnectionId,applicationuserId=Context.UserIdentifier } );
			string msg= user != null?"Connected":"please refresh";
			await Clients.Caller.SendAsync("UserConnected", Context.ConnectionId,msg);
		}

		public async Task GetGroups()
		{

			var groups=await chatService.UserAuthuicatedGroups(Context.UserIdentifier);
			foreach(var group in groups)
			{
				await Groups.AddToGroupAsync(Context.ConnectionId, group.Name);
			}
			await Clients.Caller.SendAsync("UserGroups",Context.ConnectionId,"ok");
		}
		public async Task AuthUsersToTalk()
		{
			var users=await chatService.AllowToTalk(Context.UserIdentifier);
			await Clients.Caller.SendAsync("AllowToTalk", Context.ConnectionId, users);
		}

	}
}
