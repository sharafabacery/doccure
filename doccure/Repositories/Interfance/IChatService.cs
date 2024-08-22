using doccure.Data.Models;
using doccure.Data.ResponseModels;
using doccure.Repositories.Implementaion;

namespace doccure.Repositories.Interfance
{
	public interface IChatService
	{
		public Task<UserConnected> Connect(UserConnected userConnected);
		public Task<bool> DisConnect(string ConnectionId);
		public Task<Group> AddGroup(Group group);
		public Task<UserGroups> AddUserGroups(UserGroups userGroups);
		public Task<List<GroupResponse>> GetUserGroups(string Id,List<Group> groups);
		public Task<List<Group>> UserAuthuicatedGroups(string Id);
		public Task<List<Applicationuser>> AllowToTalk(string Id);
		public Task<UserConnected> GetConnectionByUserId(string userId);
		public Task<UserGroups> IsUserActiveInGroup(string userId,int groupId);
		public Task<bool>DeActivateUserGroups(string userId);
		public Task<bool> UserConnected(string userId);
		public Task<Applicationuser> GetUser(string Id);

	}
}
