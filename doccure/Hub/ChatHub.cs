
namespace doccure.Hub
{
	using doccure.Repositories.Interfance;
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
			await Clients.Caller.SendAsync("UserGroups",Context.ConnectionId,groups);
		}

	}
}
