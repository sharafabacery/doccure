﻿using doccure.Data.Models;
using doccure.Data;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using doccure.Data.RequestModels;

namespace doccure.Repositories.Implementaion
{
	public class DoctorAppointmentService : IDoctorAppointmentService
	{
		private readonly ApplicationDbContext applicationDbContext;
		private readonly UserManager<Applicationuser> userManager;

		public DoctorAppointmentService(ApplicationDbContext applicationDbContext, UserManager<Applicationuser> userManager) {
			this.applicationDbContext = applicationDbContext;
			this.userManager = userManager;
		}

		public async Task<Booking> GetAppiontmentById(ClaimsPrincipal claims, int BookingId)
		{
			var Booking = await applicationDbContext.Bookings.Where(b => b.Id == BookingId && b.doctorId == userManager.GetUserId(claims)).FirstOrDefaultAsync();
			if (Booking == null)
			{
				return null;
			}
			else return Booking;
		}

		public async Task<List<Booking>> GetDoctorAppointmentWithinWeek(ClaimsPrincipal claims)
		{
			var DateOfToday = DateTime.Now.Date;
			var DateOfTheNextWeekOfDay = DateTime.Now.AddDays(7).Date;

			var DoctorBooking =await applicationDbContext.Bookings.Include(p => p.patient).Include(A => A.patient.address).Include(s => s.scheduleTiming).Where(b => b.doctorId == userManager.GetUserId(claims) && b.bookingDate >= DateOfToday && b.bookingDate <= DateOfTheNextWeekOfDay).OrderByDescending(d=>d.bookingDate).ToListAsync();
			return DoctorBooking;
		}

		public async Task<Booking> UpdateAppiontmentStatus(ClaimsPrincipal claims, UpdateBookingStatusRequest updateBookingStatusRequest)
		{
			var Booking = await applicationDbContext.Bookings.Where(b => b.Id == updateBookingStatusRequest.BookingId && b.doctorId == userManager.GetUserId(claims)).FirstOrDefaultAsync();
			if (Booking == null)
			{
				return null;
			}
			else
			{
				Booking.BookingStatus = (BookingStatus)updateBookingStatusRequest.Status;
				if ((BookingStatus)updateBookingStatusRequest.Status == BookingStatus.Cancel)
				{
					// give patient his/her money 
				}
				await applicationDbContext.SaveChangesAsync();
				return Booking;

			}
		}
	}
}
