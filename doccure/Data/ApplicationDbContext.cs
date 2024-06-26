﻿using doccure.Data.Models;
using doccure.Data.ResponseModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace doccure.Data
{
    public class ApplicationDbContext : IdentityDbContext<Applicationuser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Address> Address { set; get; }
        public DbSet<Speciality> Speciality { set; get; }
        public DbSet<Doctor> Doctor { set; get; }
        public DbSet<Education> Education { set; get; }
        public DbSet<Experience> Experience { set; get; }
        public DbSet<Awards> Awards { set; get; }
        public DbSet<Membership> Memberships { set; get; }
        public DbSet<Clinic> Clinics { set; get; }
        public DbSet<ClinicImage> ClinicImages { set; get; }
        public DbSet<ScheduleTiming> ScheduleTiming { set; get; }
        public DbSet<Booking> Bookings { set; get; }
        public DbSet<Prescription> Prescriptions { set; get; }
        public DbSet<MedicalRecord> MedicalRecord { set; get; }
        public DbSet<Billing> Billings { set; get; }
        public DbSet<Favourites> Favourites { set; get; }
        public DbSet<Review> Reviews { set; get; }
        public DbSet<Comment> Comments { set; get; }
        public DbSet<UserConnected> UserConnected { set; get; }
        public DbSet<Group> Groups { set; get; }
        public DbSet<UserGroups> UserGroups { set; get; }

		public virtual DbSet<DoctorSearchReturned> DoctorSearchReturned { get; set; }
		public virtual DbSet<ScheduleTimingBooking> ScheduleTimingBooking { get; set; }
		public virtual DbSet<Patients> Patients { get; set; }
		public virtual DbSet<Messages> Messages { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
			builder.Entity<DoctorSearchReturned>(e =>
			{
				e.HasNoKey().ToView("DoctorSearchReturned");
			});
			builder.Entity<ScheduleTimingBooking>(e =>
			{
				e.HasNoKey().ToView("ScheduleTimingBooking");
			});
			builder.Entity<Patients>(e =>
			{
				e.HasNoKey().ToView("Patients");
			});
			builder.Entity<Booking>()
            .HasOne(b => b.patient)
            .WithMany(a => a.PatientBooking)
            .HasForeignKey(b => b.patientId).OnDelete(DeleteBehavior.NoAction);


			builder.Entity<Booking>()
			.HasOne(b => b.doctor)
			.WithMany(a => a.DoctorBooking)
			.HasForeignKey(b => b.doctorId).OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Favourites>()
			.HasOne(b => b.patient)
			.WithMany(a => a.PatientFavourites)
			.HasForeignKey(b => b.patientId).OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Comment>()
		.HasOne(c => c.ParentComment)
		.WithMany(c => c.subComments)
		.HasForeignKey(c => c.ParentCommentId)
		.OnDelete(DeleteBehavior.Restrict); // or Cascade depending on your requirements

			builder.Entity<Messages>()
			.HasOne(b => b.Sender)
			.WithMany(a => a.SenderMessages)
			.HasForeignKey(b => b.senderId).OnDelete(DeleteBehavior.NoAction);


			builder.Entity<Messages>()
			.HasOne(b => b.Receiver)
			.WithMany(a => a.ReceiverMessages)
			.HasForeignKey(b => b.receiverId).OnDelete(DeleteBehavior.NoAction);
		}
    }
}