using doccure.Data.Models;
using doccure.Data.ResponseModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace doccure.Data
{
    public class ApplicationDbContext : IdentityDbContext<Applicationuser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Address> Address { set;get; }
        public DbSet<Speciality> Speciality { set; get; }
        public DbSet<Doctor>Doctor { set; get; }   
        public DbSet<Education> Education { set; get; }
        public DbSet<Experience> Experience { set; get; }
        public DbSet<Awards> Awards { set; get; }
        public DbSet<Membership> Memberships { set; get; }
        public DbSet<Clinic> Clinics { set; get; }
        public DbSet<ClinicImage> ClinicImages { set; get; }
        public DbSet<ScheduleTiming> ScheduleTiming { set; get; }
		public  DbSet<DoctorSearchReturned> DoctorSearchReturned { get; set; }
	}
}