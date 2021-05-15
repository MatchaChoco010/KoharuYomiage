using KoharuYomiageApp.Entities;

namespace KoharuYomiageApp.Application.Repositories.UseCases
{
    public interface IMastodonClientRepository
    {
        MastodonClient CreateMastodonClient(Instance instance, MastodonClientId clientId,
            MastodonClientSecret clientSecret);

        MastodonClient? FindMastodonClient(Instance instance);

        void SaveMastodonClient(MastodonClient mastodonClient);
    }
}
