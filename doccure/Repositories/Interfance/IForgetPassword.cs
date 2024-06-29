using doccure.Data.Models;
using doccure.Data.RequestModels;

namespace doccure.Repositories.Interfance
{
	public interface IForgetPassword
	{
		public  Task<bool> SendToken(ForgetPasswordTokenRequest s);
		public Task<Applicationuser> UpdatePassword(ForgetPasswordRequest forgetPassword);
	}
}
