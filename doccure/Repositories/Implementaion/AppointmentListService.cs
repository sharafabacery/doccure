using doccure.Data;
using doccure.Data.Models;
using doccure.Repositories.Interfance;
using Microsoft.EntityFrameworkCore;

namespace doccure.Repositories.Implementaion
{
	public class AppointmentDTO
	{
		public int Id { get; set; }
		public string PatientName { get; set; }
		public string? PatienImage { get; set; }
		public string DoctortName { get; set; }
		public string? DoctortImage { get; set; }
		public string? Speciality { get; set; }
		public BookingStatus Status { get; set; }
		public DateTime BookingDate { get; set; }
		public Double Total { get; set; }
		public string StartTime { get; set; }
		public string EndTime { get; set; }
	}
	public class AppointmentListService : IAppointmentListService
	{
		private readonly ApplicationDbContext applicationDbContext;

		public AppointmentListService(ApplicationDbContext applicationDbContext)
		{
			this.applicationDbContext = applicationDbContext;
		}
		public async Task<List<AppointmentDTO>> GetAllAppointments(bool pagination)
		{
			List<AppointmentDTO> AppointmentsLst;
			IQueryable<AppointmentDTO> Appointments =  applicationDbContext.Bookings.Include(p => p.patient).Include(d => d.doctor).Include(d => d.doctor.doctor.Speciality).Include(s=>s.scheduleTiming)
							.OrderByDescending(e => e.bookingDate).Select(e => new AppointmentDTO
							{
								Id = e.Id,
								PatientName = e.patient.FirstName + " " + e.patient.LastName,
								PatienImage = e.patient.Image,
								DoctortImage = e.doctor.Image,
								DoctortName = e.doctor.FirstName + " " + e.doctor.LastName,
								Speciality = e.doctor.doctor.Speciality.Name,
								Status =e.BookingStatus,
								BookingDate = e.bookingDate,
								Total = e.total,
								StartTime = e.scheduleTiming.StartTime,
								EndTime = e.scheduleTiming.EndTime
							});
			if (pagination) Appointments = Appointments.Take(5);
			AppointmentsLst=await Appointments.ToListAsync();
			return AppointmentsLst;
		}
	}
}
