using System.Threading.Tasks;
using KoharuYomiageApp.Application.Repositories.UseCases;
using KoharuYomiageApp.Entities;
using KoharuYomiageApp.Entities.Account;
using KoharuYomiageApp.Entities.Client.Mastodon;

namespace KoharuYomiageApp.Application.Repositories.Interfaces
{
    public class MastodonClientRepository : IMastodonClientRepository
    {
        readonly IMastodonClientStorage _storage;

        public MastodonClientRepository(IMastodonClientStorage storage)
        {
            _storage = storage;
        }

        public MastodonClient CreateMastodonClient(Instance instance, MastodonClientId clientId,
            MastodonClientSecret clientSecret)
        {
            return new(instance, clientId, clientSecret);
        }

        public async Task<MastodonClient?> FindMastodonClient(Instance instance)
        {
            var data = await _storage.FindMastodonClientData(instance.Value);
            if (data is not null)
            {
                return new MastodonClient(new Instance(data.Instance), new MastodonClientId(data.Id),
                    new MastodonClientSecret(data.Secret));
            }

            return null;
        }

        public async Task SaveMastodonClient(MastodonClient mastodonClient)
        {
            await _storage.SaveMastodonClientData(new MastodonClientData(mastodonClient.Instance.Value,
                mastodonClient.ClientId.Value, mastodonClient.ClientSecret.Value));
        }
    }
}
