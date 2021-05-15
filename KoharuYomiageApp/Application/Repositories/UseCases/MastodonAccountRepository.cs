using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoharuYomiageApp.Application.Repositories.UseCases.DataObjects;
using KoharuYomiageApp.Entities.Account;
using KoharuYomiageApp.Entities.Account.Mastodon;

namespace KoharuYomiageApp.Application.Repositories.UseCases
{
    public class MastodonAccountRepository
    {
        readonly IMastodonAccountGateway _gateway;

        public MastodonAccountRepository(IMastodonAccountGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<MastodonAccount?> FindMastodonAccount(AccountIdentifier identifier)
        {
            var data = await _gateway.FindMastodonAccountData(identifier.Value);
            if (data is not null)
            {
                return new MastodonAccount(new Username(data.Username), new Instance(data.Instance),
                    new MastodonAccessToken(data.AccessToken), new MastodonAccountIconUrl(data.IconUrl));
            }

            return null;
        }

        public MastodonAccount CreateMastodonAccount(Username username, Instance instance,
            MastodonAccessToken accessToken,
            MastodonAccountIconUrl iconUrl)
        {
            return new(username, instance, accessToken, iconUrl);
        }

        public async Task SaveMastodonAccount(MastodonAccount accountData)
        {
            await _gateway.SaveMastodonAccountData(new MastodonAccountData(accountData.Username.Value,
                accountData.Instance.Value, accountData.AccessToken.Token, accountData.IconUrl.IconUrl));
        }

        public async Task<IEnumerable<MastodonAccount>> GetMastodonAccounts()
        {
            var data = await _gateway.GetMastodonAccountData();
            return data.Select(d => new MastodonAccount(new Username(d.Username), new Instance(d.Instance),
                new MastodonAccessToken(d.AccessToken), new MastodonAccountIconUrl(d.IconUrl)));
        }
    }
}
