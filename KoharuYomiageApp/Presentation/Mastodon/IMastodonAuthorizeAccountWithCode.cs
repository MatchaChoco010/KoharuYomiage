using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.Presentation.Mastodon
{
    public interface IMastodonAuthorizeAccountWithCode
    {
        Task<string> AuthorizeWithCode(string instance, string clientId, string clientSecret, string code,
            CancellationToken cancellationToken = new());
    }
}
