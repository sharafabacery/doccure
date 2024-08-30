using Bogus;
using doccure.Data.Models;
using NuGet.Packaging;

namespace doccure.DBSeeder
{
	public enum Gender
	{
		Male=1,
		Female=0
	}
	public static class SeederEntityConfigration
	{
		private static Faker<Applicationuser> GenrateUsersConfigration()
		{
			var blood = new[] { "A", "AB", "O" };
			var userRule = new Faker<Applicationuser>()
				 .RuleFor(u => u.Gender, f => (bool)(Convert.ToBoolean((int)f.PickRandom<Gender>())))
						.RuleFor(e => e.SecurityStamp, c => Guid.NewGuid().ToString())
						 .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName(u.Gender.Value == true ? Bogus.DataSets.Name.Gender.Male : Bogus.DataSets.Name.Gender.Female))
						 .RuleFor(u => u.LastName, (f, u) => f.Name.LastName(u.Gender.Value == true ? Bogus.DataSets.Name.Gender.Male : Bogus.DataSets.Name.Gender.Female))
						 .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
						 .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
						 .RuleFor(u => u.EmailConfirmed, f => true)
						 .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
						 .RuleFor(u => u.CreatedTime, f => DateTime.Now)
						 .RuleFor(u => u.BloodGroup, f => f.PickRandom(blood));
						 //.RuleFor(u => u.PasswordHash, f => "P@ssw0rdP@ssword_cmdr");
			return userRule;
		}
		private static Faker<Address> GenrateAddressConfigration()
		{
			var addressRule = new Faker<Address>()
							.RuleFor(e => e.Address1, f => f.Address.StreetName())
							.RuleFor(e => e.City, f => f.Address.City())
							.RuleFor(e => e.Country, f => f.Address.Country())
							.RuleFor(e => e.PostalCode, f => f.Address.ZipCode())
							.RuleFor(e => e.State, f => f.Address.State());
			return addressRule;
		}
		private static Faker<Clinic> GenrateClinicConfigration()
		{
			var addressRule = new Faker<Clinic>()
							.RuleFor(e => e.FromDay, 3)
							.RuleFor(e => e.ToDay, 6)
							.RuleFor(e => e.FromTime, "09:00")
							.RuleFor(e => e.ToTime, f => "15:00")
							.RuleFor(e => e.Address, f => f.Address.StreetName())
							.RuleFor(e => e.Name, f => f.Company.CompanyName() +" Clinic");




			return addressRule;
		}
		private static Faker<Doctor> GenrateDoctorBasicInformationConfigration(List<Speciality> specialities)
		{
			var doctorRule = new Faker<Doctor>()
								.RuleFor(e => e.Speciality, f => f.PickRandom(specialities))
								.RuleFor(e => e.AboutMe, f => f.Lorem.Paragraph(20))
								.RuleFor(e => e.Price, f => ((float)f.Finance.Amount(50, 1000, 2)))
								.RuleFor(e => e.VideoCall, f => ((float)f.Finance.Amount(50, 1000, 2)))
								.RuleFor(e => e.Specialization, f => "test")
								.RuleFor(e => e.Services, f => "test");

			return doctorRule;
		}
		private static Faker<Education> GenrateEducation()
		{
			
			var education = new Faker<Education>()
							.RuleFor(e => e.Degree, f => "Bachelor's degree")
							.RuleFor(e => e.College, f => "University of " + f.Address.City() + " Faculty of Medicine")
							.RuleFor(e => e.YearofCompletion, f => f.Date.Between(new DateTime(1980, 1, 1),
	new DateTime(2010, 1, 1)));
			return education;
		}

		 private static Faker<Experience> GenrateExperience()
		{
			var experience = new Faker<Experience>()
				.RuleFor(e => e.HospitalName, f => "Hospital of " + f.Address.City())
				.RuleFor(e => e.Designation, f => f.Lorem.Paragraph(20))
				.RuleFor(e => e.From, f => f.Date.Between(new DateTime(2010, 1, 1),
	new DateTime(2016, 1, 1)))
				.RuleFor(e => e.To, (f,u) => u.From.AddYears(f.Random.Number(5)));
			return experience;
		}
		private static Faker<Awards> GenrateAwards()
		{
			var experience = new Faker<Awards>()
				.RuleFor(e => e.award, f => f.Lorem.Paragraph(20));
				
			return experience;
		}
		private static Faker<Membership> GenrateMembership()
		{
			var experience = new Faker<Membership>()
				.RuleFor(e => e.description, f => f.Lorem.Paragraph(20));

			return experience;
		}
		public static List<Applicationuser> UsersGenrated(int seed)
		{
			var usersRule = GenrateUsersConfigration();
			var AddressRule = GenrateAddressConfigration();
			var usersTest = usersRule.RuleFor(e => e.address, f => AddressRule.Generate(1).First());
			var users=usersTest.Generate(seed);
			return users;
		}
		public static List<Applicationuser> DoctorGenrated(int seed, List<Speciality> specialities)
		{
			var usersRule = GenrateUsersConfigration();
			var AddressRule = GenrateAddressConfigration();
			var ClinicRule = GenrateClinicConfigration();
			var doctorRule = GenrateDoctorBasicInformationConfigration(specialities);
			var educationRule = GenrateEducation();
			var experienceRule = GenrateExperience();
			var awardseRule = GenrateAwards();
			var MembershipRule = GenrateMembership();

			var usersTest = usersRule
				.RuleFor(e => e.address, f => AddressRule.Generate(1).First())
				.RuleFor(e => e.doctor, f => doctorRule.Generate(1).First())
				.FinishWith((x, f) =>
				{
					f.doctor.clinics.AddRange(ClinicRule.Generate(1));
					f.doctor.educations.AddRange(educationRule.Generate(1));
					f.doctor.experiences.AddRange(experienceRule.Generate(1));
					f.doctor.memberships.AddRange(MembershipRule.Generate(1));
				});
			var users = usersTest.Generate(seed);
			
			return users;
		}
	}
}
