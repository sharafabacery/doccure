using doccure.Data;
using doccure.Data.Migrations;
using doccure.Data.Models;
using doccure.Data.RequestModels;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static doccure.Program;

namespace doccure.Repositories.Implementaion
{
	public class MedicalRecordService : IMedicalRecordService
	{
		private readonly ApplicationDbContext applicationDbContext;
		private readonly UserManager<Applicationuser> userManager;
		private readonly IFileService fileService;

		public MedicalRecordService(ServiceResolver2 serviceAccessor, ApplicationDbContext applicationDbContext, UserManager<Applicationuser> userManager) {
			this.applicationDbContext = applicationDbContext;
			this.userManager = userManager;
			this.fileService = serviceAccessor("File");
		}
		public async Task<Booking> AddEditMedicalRecord(MedicalRecordRequest medicalRecordRequest)
		{
			var Book = await applicationDbContext.Bookings
														.Include(e => e.MedicalRecord)
														.Where(e => e.Id == medicalRecordRequest.BookingId)
														.FirstOrDefaultAsync();
			if(Book== null)
			{
				return null;
			}
			else
			{
				var FilePath =  fileService.SaveFile(medicalRecordRequest.ImageFile);
				if (Book.MedicalRecord!=null)
				{
					Book.MedicalRecord.Description = medicalRecordRequest.Description;
					Book.MedicalRecord.Date = medicalRecordRequest.dateTime;
					if (FilePath == "")
					{
						Book.MedicalRecord.FilePath = Book.MedicalRecord.FilePath;
					}
					else
					{
						Book.MedicalRecord.FilePath = FilePath;
					}
						
				}
				else
				{
					if (FilePath == "")
					{
						return null;
					}
					else
					{
						Book.MedicalRecord = new Data.Models.MedicalRecord()
						{
							FilePath= FilePath,
							Description= medicalRecordRequest.Description,
							Date= medicalRecordRequest.dateTime
						};
					}
				}

			}
			var res = await applicationDbContext.SaveChangesAsync();
			if (res > 0)
			{
				return Book;
			}
			else
			{
				return null;
			}
		}

		public async Task<bool> DeleteMedicalRecord(int MedicalRecordId, ClaimsPrincipal claims)
		{
			var MedicalRecorddb = await applicationDbContext.
															MedicalRecord
															.Include(b => b.booking)
															.FirstOrDefaultAsync(p => p.Id == MedicalRecordId);
			if (MedicalRecorddb == null)
			{
				return false;
			}
			else
			{
				if (MedicalRecorddb.booking.doctorId == userManager.GetUserId(claims))
				{
					applicationDbContext.Remove(MedicalRecorddb);
					await applicationDbContext.SaveChangesAsync();
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public async Task<Booking> GetMedicalRecordByBookingId(int BookingId)
		{
			var BookMedicalRecorddb = await applicationDbContext.Bookings
																.Include(p => p.MedicalRecord)
																.Where(b => b.Id == BookingId)
																.FirstOrDefaultAsync();
			if (BookMedicalRecorddb == null)
			{
				return null;
			}
			else
			{
				return BookMedicalRecorddb;
			}
		}
	}
}
