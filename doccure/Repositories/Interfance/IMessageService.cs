using doccure.Data.Models;
using doccure.Data.RequestModels;
using doccure.Data.ResponseModels;

namespace doccure.Repositories.Interfance
{
	public interface IMessageService
	{
		public Task<Messages> AddMessage(MessageRequest messageRequest, string sender);
		public Task<List<MessageDTOResponse>> GetMessages(MessageQueryRequest messageQuery);
		public Task<bool> UpdateMessage(MessageUser messageUser);
	}
}
