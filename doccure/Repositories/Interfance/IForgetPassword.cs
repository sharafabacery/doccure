using doccure.Data.RequestModels;

namespace doccure.Repositories.Interfance
{
	public interface IForgetPassword
	{
		public  Task<bool> SendToken(ForgetPasswordTokenRequest s);
	}
}
