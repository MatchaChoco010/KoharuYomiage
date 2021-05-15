using System.Threading.Tasks;

namespace KoharuYomiageApp.Application.AddMastodonAccount.Interfaces
{
    public interface IMastodonApiAuthorizeAccountWithCodeService
    {
        ValueTask<string> AuthorizeWithCode(string instance, string clientId, string clientSecret, string code);
    }
}
