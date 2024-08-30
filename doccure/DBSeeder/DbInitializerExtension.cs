using doccure.Data;
using doccure.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace doccure.DBSeeder
{
	internal static class DbInitializerExtension
	{
		public static IApplicationBuilder UseItToSeedSqlServer(this IApplicationBuilder app)
		{
			ArgumentNullException.ThrowIfNull(app, nameof(app));

			using var scope = app.ApplicationServices.CreateScope();
			var services = scope.ServiceProvider;
			try
			{
				var context = services.GetRequiredService<ApplicationDbContext>();
				var userMange = services.GetRequiredService<UserManager<Applicationuser>>();
				var roleMange = services.GetRequiredService<RoleManager<IdentityRole>>();
				DbInitializer.Initialize(context,userMange,roleMange);
			}
			catch (Exception ex)
			{

			}

			return app;
		}
	}
}
