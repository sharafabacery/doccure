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
		public async Task Signaling(string groupId,string content) {
			await Clients.OthersInGroup(groupId).SendAsync("SDP", content);
		}
	}
}
