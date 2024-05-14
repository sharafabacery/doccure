using doccure.Data.Models;

namespace doccure.Repositories.Interfance
{
	public interface IChatService
	{
		public Task<UserConnected> Connect(UserConnected userConnected);
		public Task<bool> DisConnect(string ConnectionId);
		public Task<Group> AddGroup(Group group);
		public Task<UserGroups> AddUserGroups(UserGroups userGroups);
		public Task<List<Group>> UserAuthuicatedGroups(string Id);
		public Task<List<Applicationuser>> AllowToTalk(string Id);

	}
}
