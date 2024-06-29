using doccure.Data.RequestModels;
using Org.BouncyCastle.Asn1.Pkcs;

namespace doccure.Repositories.Interfance
{
	public interface IMailService
	{
		Task<bool> SendMailAsync(MailDataRequest mailData);
	}
}
