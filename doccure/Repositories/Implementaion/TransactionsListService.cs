using doccure.Data;
using doccure.Data.Models;
using doccure.Repositories.Interfance;
using Microsoft.EntityFrameworkCore;

namespace doccure.Repositories.Implementaion
{
	public class TransactionDTO{
		public int TransactionId { get; set; }
		public string PatientId { get; set; }
		public string Image { get; set; }
		public string PatientName { get; set; }
		public double ToTal { get; set; }
		public string Status { get; set; }


	}
public class TransactionsListService : ITransactionsListService
{
		private readonly ApplicationDbContext applicationDbContext;

		public TransactionsListService(ApplicationDbContext applicationDbContext)
		{
			this.applicationDbContext = applicationDbContext;
		}
		public async Task<List<TransactionDTO>> GetAllTransactionss()
	{
			var transactions = await applicationDbContext.Bookings
			.Include(u => u.patient) // Include the Department navigation property
			.Include(u => u.Billing) // Include the Department navigation property
			
			.Where(e=>e.Billing.Count>0&&(e.BookingStatus== BookingStatus.Accepted|| e.BookingStatus == BookingStatus.Complete)) // Filter users with the "employee" role

			.Select(e =>

				new TransactionDTO
				{
					PatientId=e.patientId,
					TransactionId=e.Id,
					Image=e.patient.Image,
					PatientName=e.patient.FirstName+' '+e.patient.LastName,
					ToTal=e.total,
					Status=e.BookingStatus.ToString()
				}

			)
			.ToListAsync();
			return transactions;
	}
}
}
