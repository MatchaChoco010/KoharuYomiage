using System.Threading.Tasks;
using KoharuYomiageApp.Entities;
using KoharuYomiageApp.Entities.Account;
using KoharuYomiageApp.Entities.Client.Mastodon;

namespace KoharuYomiageApp.Application.Repositories.UseCases
{
    public interface IMastodonClientRepository
    {
        MastodonClient CreateMastodonClient(Instance instance, MastodonClientId clientId,
            MastodonClientSecret clientSecret);

        ValueTask<MastodonClient?> FindMastodonClient(Instance instance);

        ValueTask SaveMastodonClient(MastodonClient mastodonClient);
    }
}
