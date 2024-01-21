using doccure.Data.Models;
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

		public virtual DbSet<DoctorSearchReturned> DoctorSearchReturned { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
			builder.Entity<DoctorSearchReturned>(e =>
			{
				e.HasNoKey().ToView("DoctorSearchReturned");
			});
            builder.Entity<Booking>()
            .HasOne(b => b.patient)
            .WithMany(a => a.PatientBooking)
            .HasForeignKey(b => b.patientId).OnDelete(DeleteBehavior.NoAction);


			builder.Entity<Booking>()
			.HasOne(b => b.doctor)
			.WithMany(a => a.DoctorBooking)
			.HasForeignKey(b => b.doctorId).OnDelete(DeleteBehavior.NoAction);
		}
    }
}