using doccure.Data;
using doccure.Data.Models;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace doccure.Repositories.Implementaion
{
	public class StaticticsService : IStaticticsService
	{
		private readonly UserManager<Applicationuser> userManager;
		private readonly ApplicationDbContext applicationDbContext;

		public StaticticsService(UserManager<Applicationuser> userManager, ApplicationDbContext applicationDbContext)
		{
			this.userManager = userManager;
			this.applicationDbContext = applicationDbContext;
		}
		public async Task<int> GetAppoinmentCount()
		{
			var countAppoinments = await applicationDbContext.Bookings.CountAsync();
			return countAppoinments;
		}

		public async Task<double> GetRevnueTotal()
		{
			var RevnueTotal = await applicationDbContext.Bookings.SumAsync(e=>e.total);
			return RevnueTotal;
		}

		public async Task<int> GetUserCountByRole(string role)
		{
			var usersInRole = await userManager.GetUsersInRoleAsync(role);

			// Count the number of users
			int numberOfUsers = usersInRole.Count;

			return numberOfUsers;
		}

		public async Task<List<KeyValuePair<string, int>>> GetUsersYears()
		{
			var counter=await applicationDbContext.Users.Include(D=>D.doctor).GroupBy(U=>U.CreatedTime.Value.Year).ToDictionaryAsync(g => g.Key.ToString(), g => g.Count());

			List<KeyValuePair<string, int>> xx = counter.ToList();
			return xx;
		}

		public async Task<List<KeyValuePair<string, double>>> Revenue()
		{
			var revnue = await applicationDbContext.Bookings.GroupBy(u => u.bookingDate.Year).ToDictionaryAsync(g => g.Key.ToString(), g => g.Sum(e => e.total));
			List<KeyValuePair<string, double>> list = revnue.ToList();
			return list;
		}
	}
}
