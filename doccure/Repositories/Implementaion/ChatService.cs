﻿using doccure.Data;
using doccure.Data.Models;
using doccure.Data.RequestModels;
using doccure.Data.ResponseModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace doccure.Repositories.Implementaion
{
	public class GroupResponse
	{
		public Group Group { set; get; }
		public Applicationuser? User { set; get; }
		public bool Online { set; get; }

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
			var authGroups = await this.UserAuthuicatedGroups(Id);
			var talking = authGroups.Select(e=>new string( e.Name.Replace(Id,""))).ToList();
			var role = await userManager.GetRolesAsync(user);
			var patient = role.FirstOrDefault(e => e == "patient");
			var allowedTalk = await applicationDbContext.Bookings
				.Where(p => p.doctorId == Id || p.patientId == Id)
				.Select( p =>patient==null?p.patient:p.doctor).ToListAsync();
			var finalAllowedTalk = allowedTalk.Where(e=>!talking.Contains(e.Id)).ToList();
			return finalAllowedTalk;
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

		public async Task<bool> DeActivateUserGroups(string userId)
		{
			var res = await applicationDbContext.UserGroups.Where(e => e.applicationuserId == userId).ExecuteUpdateAsync(c => c.SetProperty(cc => cc.Active, false));
			if (res > 0)
			{
				return true;
			}
			else
			{
				return false;
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
			var connection = await applicationDbContext.UserConnected
														.Include(e=>e.applicationuser)
														.Where(e => e.applicationuserId == userId)
														
														.FirstOrDefaultAsync();
			if (connection != null) return connection;
			else return null;
		}

		public async Task<UserDTO> GetUser(string Id)
		{
			var user=await userManager.FindByIdAsync(Id);
			if (user != null)
			{
				var userDTO = new UserDTO();
				userDTO.Id = user.Id;
				userDTO.ProfileImage = user.Image;
				userDTO.FullName = user.FirstName + ' ' + user.LastName;
				return userDTO;
			}
			else
			{
				return null;
			}
		}

		public async Task<List<GroupResponse>> GetUserGroups(string Id,List<Group> groups)
		{
			var usersTalk = await applicationDbContext.UserGroups.Include(p => p.applicationuser).Include(p=>p.group).Where(e => groups.Any(c => c.Equals(e.group))&&e.applicationuserId!=Id).Select(e=>new GroupResponse { Group=e.group,User=e.applicationuser,Online=e.Active}).ToListAsync();
			return usersTalk;
			
		}

		public  async Task<UserGroups> IsUserActiveInGroup(string userId, int groupId)
		{
			var res = await applicationDbContext.UserGroups
							.Where(e => e.GroupId == groupId && e.applicationuserId == userId).FirstOrDefaultAsync();
			if (res != null)
			{
				res.Active = !res.Active;
			}
			var result = await applicationDbContext.SaveChangesAsync();
			if (result > 0) return res;
			//var usersConnected = await applicationDbContext.UserGroups.Include(e => e.applicationuser.UserConnecteds).Where(e => e.GroupId == Id);
			return null;
		}

		public async Task<List<Group>> UserAuthuicatedGroups(string Id)
		{
			//var group = await applicationDbContext.UserGroups.Include(p => p.group).Include(p => p.group.user).Where(p => p.applicationuserId == Id).ToListAsync();
			var groups =await applicationDbContext.UserGroups.Include(p=>p.group).Include(p=>p.group.user).Where(p=>p.applicationuserId==Id).Select(e=>e.group).ToListAsync();
			return groups;
		}

		public async Task<bool> UserConnected(string userId)
		{
			var Connected = await applicationDbContext.UserConnected
														.Where(e => e.applicationuserId == userId)
											.FirstOrDefaultAsync();
			return Connected!=null;
			
		}
	}
}
