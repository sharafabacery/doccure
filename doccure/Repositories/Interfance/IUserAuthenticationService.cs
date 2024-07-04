using doccure.Data.RequestModels;
using System.Security.Claims;

namespace doccure.Repositories.Interfance
{
    public interface IUserAuthenticationService
    {
        Task<bool> RegisterAsync(RegisterRequest registerRequest);
		Task<bool> LoginAsync(LoginRequest loginModel);
        Task<bool> LoginExtnal(ExternalLoginRequestcs loginModel);
        Task<bool> RegisterExtnal(ExternalLoginRequestcs loginModel);

		Task LogoutAsync();
	}
}
