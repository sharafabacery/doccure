using doccure.Data;
using doccure.Data.Models;
using doccure.Data.RequestModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace doccure.Repositories.Implementaion
{
	public class GroupResponse
	{
		public Group? Group { set; get; }
		public Applicationuser? User { set; get; }

	}
	public class ChatService : IChatService
	{
		private readonly UserManager<Applicationuser> userManager;
		private readonly ApplicationDbContext applicationDbContext;

		public ChatService(UserManager<Applicationuser> userManager, ApplicationDbContext applicationDbContext)
		{
			this.userManager = userManager;
			this.applicationDbContext = applicationDbContext;
		}
		public async Task<Group> AddGroup(Group group)
		{
			var groupFound = await applicationDbContext.Groups.Where(p => p.Name==group.Name).FirstOrDefaultAsync();
			if(groupFound != null)
			{
				return groupFound;
			}
			else
			{
				groupFound = new Group();
				groupFound.Name = group.Name;
				await applicationDbContext.Groups.AddAsync(groupFound);

				var res = await applicationDbContext.SaveChangesAsync();

				if (res > 0)
				{
					return groupFound;
				}
				else
				{
					return null;
				}
			}

		}

		public async Task<UserGroups> AddUserGroups(UserGroups userGroups)
		{
			var groupFound = await applicationDbContext.
				UserGroups.
				Where(p => p.applicationuserId==userGroups.applicationuserId&&p.GroupId==userGroups.GroupId).FirstOrDefaultAsync();
			if (groupFound != null)
			{
				return groupFound;
			}
			else
			{
				groupFound = new UserGroups();
				groupFound = userGroups;
				await applicationDbContext.UserGroups.AddAsync(groupFound);
				var res = await applicationDbContext.SaveChangesAsync();

				if (res > 0)
				{
					return groupFound;
				}
				else
				{
					return null;
				}
			}
		}

		public async Task<List<Applicationuser>> AllowToTalk(string Id)
		{
			var user = await applicationDbContext.Users.FirstOrDefaultAsync(r=>r.Id==Id);
			var role = await userManager.GetRolesAsync(user);
			var patient = role.FirstOrDefault(e => e == "patient");
			var allowedTalk = await applicationDbContext.Bookings.Where(p => p.doctorId == Id || p.patientId == Id).Select( p =>patient==null?p.patient:p.doctor).ToListAsync();
			return allowedTalk;
		}

		public async Task<UserConnected> Connect(UserConnected userConnected)
		{
			var groupFound = await applicationDbContext.
				UserConnected.
				Where(p => p.applicationuserId == userConnected.applicationuserId && p.ConnectionID == userConnected.ConnectionID).FirstOrDefaultAsync();
			if (groupFound != null)
			{
				return groupFound;
			}
			else
			{
				groupFound = new UserConnected();
				groupFound = userConnected;
				await applicationDbContext.UserConnected.AddAsync(groupFound);
				var res = await applicationDbContext.SaveChangesAsync();

				if (res > 0)
				{
					return groupFound;
				}
				else
				{
					return null;
				}
			}
		}

		public async Task<bool> DisConnect(string ConnectionId)
		{
			var connetion = await applicationDbContext.UserConnected.FirstOrDefaultAsync(e => e.ConnectionID == ConnectionId);
			if (connetion == null)
			{
				return false;
			}
			else
			{
				
					applicationDbContext.Remove(connetion);
					await applicationDbContext.SaveChangesAsync();
					return true;

			}
		}

		public async Task<UserConnected> GetConnectionByUserId(string userId)
		{
			var connection = await applicationDbContext.UserConnected.Where(e => e.applicationuserId == userId).FirstOrDefaultAsync();
			if (connection != null) return connection;
			else return null;
		}

		public async Task<List<GroupResponse>> GetUserGroups(string Id,List<Group> groups)
		{
			var usersTalk = await applicationDbContext.UserGroups.Include(p => p.applicationuser).Include(p=>p.group).Where(e => groups.Any(c => c.Equals(e.group))&&e.applicationuserId!=Id).Select(e=>new GroupResponse { Group=e.group,User=e.applicationuser}).ToListAsync();
			return usersTalk;
			
		}

		public  async Task<bool> IsGroupConnected(int Id)
		{
			//var usersConnected = await applicationDbContext.UserGroups.Include(e => e.applicationuser.UserConnecteds).Where(e => e.GroupId == Id);
			return false;
		}

		public async Task<List<Group>> UserAuthuicatedGroups(string Id)
		{
			//var group = await applicationDbContext.UserGroups.Include(p => p.group).Include(p => p.group.user).Where(p => p.applicationuserId == Id).ToListAsync();
			var groups =await applicationDbContext.UserGroups.Include(p=>p.group).Include(p=>p.group.user).Where(p=>p.applicationuserId==Id).Select(e=>e.group).ToListAsync();
			return groups;
		}
	}
}
