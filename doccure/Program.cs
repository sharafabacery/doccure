using doccure.Data;
using doccure.Data.Models;
using doccure.Repositories.Implementaion;
using doccure.Repositories.Interfance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace doccure
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddIdentity<Applicationuser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();


            builder.Services.ConfigureApplicationCookie(op => op.LoginPath = "/UserAuthuntication/Login");
            builder.Services.AddScoped<IUserAuthenticationService, UserAuthunticationService>();
            builder.Services.AddScoped<IUserProfileSettingsService,UserProfileSettingsService>();
            builder.Services.AddScoped<IUserProfileSettingsService,DoctorProfileSettingsService>();
            builder.Services.AddScoped<ISpecalityService,SpecalityService>();
            builder.Services.AddScoped<IDeleteEducation, DeleteEducationService>();
			builder.Services.AddScoped<IDeleteExperience, DeleteExperienceService>();
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
            

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            //app.MapRazorPages();

            app.Run();
        }
    }
}