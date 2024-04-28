using doccure.Repositories.Implementaion;

namespace doccure.Repositories.Interfance
{
	public interface ITransactionsListService
	{
		public Task<List<TransactionDTO>> GetAllTransactionss();

		public Task<TransactionDTO> GetTransaction(int id);
	}
}
