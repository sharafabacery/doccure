using doccure.Data.RequestModels;

namespace doccure.Repositories.Interfance
{
    public interface IUserAuthenticationService
    {
        Task<bool> RegisterAsync(RegisterRequest registerRequest);
		Task<bool> LoginAsync(LoginRequest loginModel);

        Task LogoutAsync();
	}
}
