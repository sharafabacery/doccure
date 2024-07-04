using doccure.Data;
using doccure.Data.Models;
using doccure.Repositories.Implementaion;
using doccure.Repositories.Interfance;
using Google.Apis.Auth.AspNetCore3;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using static doccure.Repositories.Implementaion.DoctorDTO;

namespace doccure
{
    public class Program
    {
		public delegate IUserProfileSettingsService ServiceResolver(string key);
		public delegate IFileService ServiceResolver2(string key);
		public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
			
			// Add services to the container.
			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

			
		builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddIdentity<Applicationuser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
			builder.Services.AddAuthentication(o =>
			{
				
				o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			})
		.AddCookie().AddGoogleOpenIdConnect(googleOptions =>
			{
				googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
				googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];

				
			});

			//builder.Services.ConfigureApplicationCookie(op => op.LoginPath = "/UserAuthuntication/Login");
			builder.Services.ConfigureApplicationCookie(op => { op.LoginPath = "/UserAuthuntication/Login"; op.Cookie.Name = "authCookie"; });
			//builder.Services.ConfigureApplicationCookie(op => op.LoginPath = "/UserAuthuntication/LoginGoogle");
			//builder.Services.ConfigureExternalCookie(op => op.LoginPath = "/UserAuthuntication/LoginGoogle");
			builder.Services.AddLogging(loggingBuilder =>
			{
				loggingBuilder.AddConsole();
				loggingBuilder.AddDebug();
			});

			builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailProvider"));
			builder.Services.AddTransient<IMailService, MailService>();
			builder.Services.AddScoped<IUserAuthenticationService, UserAuthunticationService>();
            builder.Services.AddScoped<UserProfileSettingsService>();
            builder.Services.AddScoped<DoctorProfileSettingsService>();
            builder.Services.AddScoped<ImageService>();
            builder.Services.AddScoped<FileService>();
			builder.Services.AddScoped<ServiceResolver>(serviceProvider => key =>
			{
				switch (key)
				{
					case "Patient":
						return serviceProvider.GetService<UserProfileSettingsService>();
					case "admin":
						return serviceProvider.GetService<UserProfileSettingsService>();
					case "Doctor":
						return serviceProvider.GetService<DoctorProfileSettingsService>();
					
					default:
						throw new KeyNotFoundException(); // or maybe return null, up to you
				}
			});
			builder.Services.AddScoped<ServiceResolver2>(serviceProvider => key =>
			{
				switch (key)
				{
					case "Image":
						return serviceProvider.GetService<ImageService>();
					case "File":
						return serviceProvider.GetService<FileService>();

					default:
						throw new KeyNotFoundException(); // or maybe return null, up to you
				}
			});
			
			builder.Services.AddScoped<ISpecalityService,SpecalityService>();
            builder.Services.AddScoped<IDeleteEducation, DeleteEducationService>();
			builder.Services.AddScoped<IDeleteExperience, DeleteExperienceService>();
			builder.Services.AddScoped<IDeleteAwardService, DeleteAwaradService>();
			builder.Services.AddScoped<IDeleteMembershipService, DeleteMembershipService>();
			builder.Services.AddScoped<IScheduleTimingService, ScheduleTimingService>();
			builder.Services.AddScoped<IDoctorClinicService, DoctorClinicService>();
			builder.Services.AddScoped<IClinicService, ClinicService>();
			builder.Services.AddScoped<IDoctorSearch, DoctorSearchService>();
			builder.Services.AddScoped<IBookingService, BookingService>();
			builder.Services.AddScoped<IPatientsService, PatientsService>();
			builder.Services.AddScoped<IDoctorAppointmentService, DoctorAppointmentService>();
			builder.Services.AddScoped<IDoctorDashboardService, DoctorDashboardService>();
			builder.Services.AddScoped<IPatientAppointmentProfile, PatientAppointmentProfileService>();
			builder.Services.AddScoped<IPatientAppiontmentProfileDoctorSide, PatientAppiontmentProfileDoctorSideService>();
			builder.Services.AddScoped<ILastAppointmentofPatient, LastAppointmentofPatientService>();
			builder.Services.AddScoped<ILastMedicalRecordBookingPatientService, LastMedicalRecordBookingPatientService>();
			builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();
			builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
			builder.Services.AddScoped<ILastBillingService, LastBillingService>();
			builder.Services.AddScoped<IBillingService, BillingService>();
			builder.Services.AddScoped<IPatientAppiontmentProfile, PatientAppiontmentProfile>();
			builder.Services.AddScoped<IFavouritesServcie, FavouritesServcie>();
			builder.Services.AddScoped<IReviewService, ReviewService>();
			builder.Services.AddScoped<IReviewCommentService, ReviewCommentService>();
			builder.Services.AddScoped<IDoctorListDashboardService, DoctorListDashboardService>();
			builder.Services.AddScoped<IPatientListService, PatientListService>();
			builder.Services.AddScoped<ITransactionsListService, TransactionsListService>();
			builder.Services.AddScoped<IReviewListService, ReviewListService>();
			builder.Services.AddScoped<IAppointmentListService, AppointmentListService>();
			builder.Services.AddScoped<IStaticticsService, StaticticsService>();
			builder.Services.AddScoped<IForgetPassword, ForgetPasswordService>();
			// builder.Services.AddScoped<IUserAuthticationService, UserAuthticationService>();

			//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
			//     .AddEntityFrameworkStores<ApplicationDbContext>();
			builder.Services.AddControllersWithViews();
			//         builder.Services.AddCors(options =>
			//{
			//	options.AddPolicy("AllowAll", builder =>
			//	{
			//		builder.AllowAnyOrigin()
			//			   .AllowAnyMethod()
			//			   .AllowAnyHeader();
			//	});
			//});
			builder.Services.AddSignalR();
			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
			//app.UseCors("AllowAll");
			app.UseAuthentication();
            app.UseAuthorization();

			app.MapAreaControllerRoute(
				name: "AdminCorner",
				areaName: "AdminCorner",
				pattern: "AdminCorner/{controller=Home}/{action=Index}/{id?}");

			app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            //app.MapRazorPages();

            app.Run();
        }
    }
}