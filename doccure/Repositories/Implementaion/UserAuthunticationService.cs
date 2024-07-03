using doccure.Data.Models;
using doccure.Data.RequestModels;
using doccure.Repositories.Interfance;
using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace doccure.Repositories.Implementaion
{
    public class UserAuthunticationService : IUserAuthenticationService
    {
        private readonly SignInManager<Applicationuser> signInManager;
        private readonly UserManager<Applicationuser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserAuthunticationService(SignInManager<Applicationuser> signInManager, UserManager<Applicationuser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

		public async Task<bool> LoginAsync(LoginRequest loginModel)
		{
            bool result = false;
			var user = await userManager.FindByEmailAsync(loginModel.Email);
			if (user != null)
			{
				if (await userManager.CheckPasswordAsync(user, loginModel.Password))
			{
					var signInUser = await signInManager.PasswordSignInAsync(user, loginModel.Password, false, true);
					if (signInUser.Succeeded)
					{
						var userRoles = await userManager.GetRolesAsync(user);
						var authClaims = new List<Claim> {
				new Claim(ClaimTypes.Name,user.UserName),
				new Claim(ClaimTypes.NameIdentifier,user.Id),

				};
						foreach (var userRole in userRoles)
						{
							authClaims.Add(new Claim(ClaimTypes.Role, userRole));
						}
                        
                        result = true;
					}
					
				}
			}
            return result;
			
			
		}

        public async Task<bool> LoginExtnal(ExternalLoginRequestcs loginModel)
        {
            bool result = false;
            var userExists = await userManager.FindByEmailAsync(loginModel.Email);
            if (userExists == null)
            {
				Applicationuser applicationUser = new Applicationuser
				{
					SecurityStamp = Guid.NewGuid().ToString(),

					Email = loginModel.Email,
					UserName = loginModel.UserName.Replace(" ", string.Empty),
					EmailConfirmed = true,
					CreatedTime = DateTime.Now,
					FirstName=loginModel.Name,
					Image=loginModel.Picture,
					LastName=loginModel.FamilyName
                    
				};
				var userCreation = await userManager.CreateAsync(applicationUser);
				if (userCreation.Succeeded)
				{
					if (!await roleManager.RoleExistsAsync(loginModel.Type))
					{
						await roleManager.CreateAsync(new IdentityRole(loginModel.Type));
					}
					if (await roleManager.RoleExistsAsync(loginModel.Type))
					{
						await userManager.AddToRoleAsync(applicationUser, loginModel.Type);
					}
					result = true;
				}
				else
				{
					result = false;
				}
			}
			else
			{
				var userRoles = await userManager.GetRolesAsync(userExists);
				var authClaims = new List<Claim> {
				new Claim(ClaimTypes.Name,userExists.UserName),
				new Claim(ClaimTypes.NameIdentifier,userExists.Id),

				};
				foreach (var userRole in userRoles)
				{
					authClaims.Add(new Claim(ClaimTypes.Role, userRole));
				}
				//await 

				result = true;
			}
			return result;
		}

		public async Task LogoutAsync()
		{
			await signInManager.SignOutAsync();

		}

		public async Task<bool> RegisterAsync(RegisterRequest registerRequest)
        {
            bool result = false;
            var userExists = await userManager.FindByEmailAsync(registerRequest.Email);
            if (userExists == null)
            {
                Applicationuser applicationUser = new Applicationuser
                {
                    SecurityStamp = Guid.NewGuid().ToString(),

                    Email = registerRequest.Email,
                    UserName = registerRequest.Username,
                    EmailConfirmed = true,
                    CreatedTime = DateTime.Now

				};
                var userCreation = await userManager.CreateAsync(applicationUser, registerRequest.Password);
                if (userCreation.Succeeded) 
                { 
                    if (!await roleManager.RoleExistsAsync(registerRequest.Role))
                {
                    await roleManager.CreateAsync(new IdentityRole(registerRequest.Role));
                }
                if (await roleManager.RoleExistsAsync(registerRequest.Role))
                {
                    await userManager.AddToRoleAsync(applicationUser, registerRequest.Role);
                }
                    result = true;
                }
                
            
        }
            

            
            return result;
        }
    }
}
