namespace doccure.Hub
{
	using doccure.Data.Models;
	using doccure.Data.RequestModels;
	using doccure.Repositories.Implementaion;
	using doccure.Repositories.Interfance;
	using Google.Apis.Drive.v3.Data;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.SignalR;
	using System.Security.Claims;
	using System.Threading.Tasks;
	using System.Transactions;

	public class MediaHub : Hub
	{
	}
}
