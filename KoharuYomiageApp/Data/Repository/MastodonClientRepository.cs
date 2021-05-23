using System.Threading.Tasks;
using KoharuYomiageApp.Data.Repository.DataObjects;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.Client.Mastodon;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.Data.Repository
{
    public class MastodonClientRepository : IMastodonClientRepository
    {
        readonly IMastodonClientStorage _storage;

        public MastodonClientRepository(IMastodonClientStorage storage)
        {
            _storage = storage;
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

        public MastodonClient CreateMastodonClient(Instance instance, MastodonClientId clientId,
            MastodonClientSecret clientSecret)
        {
            return new(instance, clientId, clientSecret);
        }

        public async Task SaveMastodonClient(MastodonClient client)
        {
            await _storage.SaveMastodonClientData(new MastodonClientData(client.Instance.Value, client.ClientId.Value,
                client.ClientSecret.Value));
        }
    }
}
