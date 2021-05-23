using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.Presentation.Mastodon
{
    public interface IMastodonRegisterClient
    {
        Task<(string, string)> RegisterClient(string instance, CancellationToken cancellationToken = new());
    }
}
