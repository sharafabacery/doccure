namespace doccure.Hub
{
	using doccure.Data.Models;
	using doccure.Data.RequestModels;
	using doccure.Repositories.Implementaion;
	using doccure.Repositories.Interfance;
	using Google.Apis.Drive.v3.Data;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.SignalR;
	using System.Runtime.Intrinsics.Arm;
	using System.Security.Claims;
	using System.Threading.Tasks;
	using System.Transactions;

	public class MediaHub : Hub
	{
		public async Task AddConnection(string groupId)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
			await Clients.Caller.SendAsync("Info", "user Connected");
		}
		
		
		public async Task SendOffer(string groupId,string offer)
		{
			await Clients.OthersInGroup(groupId).SendAsync("receiveOffer", offer);
		}

		public async Task SendAnswer(string groupId,string answer)
		{
			await Clients.OthersInGroup(groupId).SendAsync("receiveAnswer", answer);
		}

		public async Task SendIceCandidate(string groupId,string candidate)
		{
			await Clients.OthersInGroup(groupId).SendAsync("receiveIceCandidate", candidate);
		}
		public override async Task OnConnectedAsync()
		{
			await Clients.Caller.SendAsync("AddConection");
			await base.OnConnectedAsync();
		}
	}
}
