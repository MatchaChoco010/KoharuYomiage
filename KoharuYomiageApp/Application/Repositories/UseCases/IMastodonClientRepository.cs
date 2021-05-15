using System.Threading.Tasks;
using KoharuYomiageApp.Entities;

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
