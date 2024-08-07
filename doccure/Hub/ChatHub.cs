
namespace doccure.Hub
{
	using doccure.Repositories.Interfance;
	using Google.Apis.Drive.v3.Data;
	using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Threading.Tasks;
	using System.Transactions;

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
		public async Task AddUserToGroup(string user1)
		{
			string msg = "error, please refresh the page";
			using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
			{
				try
				{
					var group=await chatService.AddGroup(new Data.Models.Group { Name= Context.UserIdentifier +user1 });
					if(group!= null)
					{
						 await chatService.AddUserGroups(new Data.Models.UserGroups() {group=group,applicationuserId= Context.UserIdentifier });
						await chatService.AddUserGroups(new Data.Models.UserGroups() {group=group,applicationuserId= user1 });
						msg = "ok,please close the page";
						scope.Complete();

					}
					else
					{
						throw new Exception("can not update doctor profile");
					}
				}catch(Exception ex)
				{
					scope.Dispose();
				}

			}
			await Clients.Caller.SendAsync("Info", Context.ConnectionId, msg);

		}
		public async Task GetGroups()
		{

			var groups=await chatService.UserAuthuicatedGroups(Context.UserIdentifier);
			foreach(var group in groups)
			{
				await Groups.AddToGroupAsync(Context.ConnectionId, group.Group.Name);
			}
			await Clients.Caller.SendAsync("UserGroups",Context.ConnectionId,groups);
		}
		public async Task AuthUsersToTalk()
		{
			var users=await chatService.AllowToTalk(Context.UserIdentifier);
			await Clients.Caller.SendAsync("AllowToTalk", Context.ConnectionId, users);
		}
		public async Task RemoveConnection()
		{
			await chatService.DisConnect(Context.ConnectionId);
		}
		public override async Task OnConnectedAsync()
		{
			
			await AddConnection();
			await AuthUsersToTalk();
			await GetGroups();
			await base.OnConnectedAsync();

		}
		public override async Task OnDisconnectedAsync(Exception exception)
		{
			await RemoveConnection();
			await base.OnDisconnectedAsync(exception);
		}

	}
}
