using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.Client.Mastodon;

namespace KoharuYomiageApp.UseCase.Repository
{
    public interface IMastodonClientRepository
    {
        Task<MastodonClient?> FindMastodonClient(Instance instance, CancellationToken cancellationToken);

        MastodonClient CreateMastodonClient(Instance instance, MastodonClientId clientId,
            MastodonClientSecret clientSecret);

        Task SaveMastodonClient(MastodonClient client, CancellationToken cancellationToken);
    }
}
