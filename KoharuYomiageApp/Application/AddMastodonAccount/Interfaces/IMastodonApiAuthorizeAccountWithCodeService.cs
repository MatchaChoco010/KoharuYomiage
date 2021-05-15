using System.Threading.Tasks;

namespace KoharuYomiageApp.Application.AddMastodonAccount.Interfaces
{
    public interface IMastodonApiAuthorizeAccountWithCodeService
    {
        Task<string> AuthorizeWithCode(string instance, string clientId, string clientSecret, string code);
    }
}
