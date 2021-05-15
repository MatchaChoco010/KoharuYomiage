using System.Threading.Tasks;
using KoharuYomiageApp.Application.Repositories.UseCases.DataObjects;
using KoharuYomiageApp.Entities.Account;
using KoharuYomiageApp.Entities.Client.Mastodon;

namespace KoharuYomiageApp.Application.Repositories.UseCases
{
    public class MastodonClientRepository
    {
        readonly IMastodonClientGateway _gateway;

        public MastodonClientRepository(IMastodonClientGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<MastodonClient?> FindMastodonClient(Instance instance)
        {
            var data = await _gateway.FindMastodonClientData(instance.Value);
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
            await _gateway.SaveMastodonClientData(new MastodonClientData(client.Instance.Value, client.ClientId.Value,
                client.ClientSecret.Value));
        }
    }
}
