
namespace doccure.Hub
{
	using doccure.Data.Models;
	using doccure.Data.RequestModels;
	using doccure.Repositories.Interfance;
	using Google.Apis.Drive.v3.Data;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Threading.Tasks;
	using System.Transactions;

	public class ChatHub : Hub
	{
		private readonly IChatService chatService;
		private readonly IMessageService messageService;

		public ChatHub(IChatService chatService,IMessageService messageService)
		{
			this.chatService = chatService;
			this.messageService = messageService;
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
			//var groupTalk = await chatService.GetUserGroups(Context.UserIdentifier, groups);

			foreach (var group in groups)
			{
				await Groups.AddToGroupAsync(Context.ConnectionId, group.Name);
			}
			await Clients.Caller.SendAsync("Groups",Context.ConnectionId, groups);
		}
		public async Task GetUserGroups(List<Group> groups)
		{
			var groupTalk = await chatService.GetUserGroups(Context.UserIdentifier, groups);
			await Clients.Caller.SendAsync("UserGroups", Context.ConnectionId, groupTalk);
		}
		public async Task AuthUsersToTalk()
		{
			var users=await chatService.AllowToTalk(Context.UserIdentifier);
			await Clients.Caller.SendAsync("AllowToTalk", Context.ConnectionId, users);
		}
		public async Task GetMessages( MessageQueryRequest messageQueryRequest)
		{
			messageQueryRequest.Sender = Context.UserIdentifier;
		
			var messages = await messageService.GetMessages(messageQueryRequest);
			await Clients.Caller.SendAsync("MessagesSendClient", Context.ConnectionId, messages);
		}
		public async Task MarkRead( MessageUser messageUser)
		{
			messageUser.Sender = Context.UserIdentifier;
			var res = await messageService.UpdateMessage(messageUser);
			await Clients.Group(messageUser.GroupName).SendAsync("MessagesReadClient", Context.ConnectionId, res,messageUser.MessageId);
		}
		public async Task AddMessage(MessageRequest messageRequest)
		{
			var res = await messageService.AddMessage(messageRequest, Context.UserIdentifier);
			await Clients.Group(messageRequest.GroupName).SendAsync("MessageSent", Context.ConnectionId, res);
		}
		public async Task ActivationUserInGroup(int groupId)
		{
			var res = await chatService.IsUserActiveInGroup(Context.UserIdentifier, groupId);
			if (res!=null)
			{
				await Clients.Caller.SendAsync("Info", Context.ConnectionId, res);
			}
			else
			{
				await Clients.Caller.SendAsync("Info", Context.ConnectionId, "error happen");
			}
		}
		public async Task RemoveConnection()
		{
			await chatService.DisConnect(Context.ConnectionId);
		}
		public async Task RemoveGroups()
		{
			var groups = await chatService.UserAuthuicatedGroups(Context.UserIdentifier);
			foreach (var group in groups)
			{
				await Groups.RemoveFromGroupAsync(Context.ConnectionId,group.Name);
			}
			
		}
		public async Task DeactivateUserGroups()
		{
			await chatService.DeActivateUserGroups(Context.UserIdentifier);
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
			await RemoveGroups();
			await DeactivateUserGroups();
			await RemoveConnection();
			await base.OnDisconnectedAsync(exception);
		}

	}
}
