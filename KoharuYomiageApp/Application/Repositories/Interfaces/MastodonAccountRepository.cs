using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoharuYomiageApp.Application.Repositories.UseCases;
using KoharuYomiageApp.Entities;
using KoharuYomiageApp.Entities.Account;
using KoharuYomiageApp.Entities.Account.Mastodon;

namespace KoharuYomiageApp.Application.Repositories.Interfaces
{
    public class MastodonAccountRepository : IMastodonAccountRepository
    {
        readonly IMastodonAccountStorage _storage;

        public MastodonAccountRepository(IMastodonAccountStorage storage)
        {
            _storage = storage;
        }

        public async Task<MastodonAccount?> FindMastodonAccount(AccountIdentifier identigier)
        {
            var accountData = await _storage.FindMastodonAccountData(identigier.Value);
            if (accountData is not null)
            {
                return new MastodonAccount(new Username(accountData.Username), new Instance(accountData.Instance),
                    new MastodonAccessToken(accountData.AccessToken),
                    new MastodonAccountIconUrl(accountData.IconUrl));
            }

            return null;
        }

        public MastodonAccount CreateMastodonAccount(Username username, Instance instance,
            MastodonAccessToken accessToken, MastodonAccountIconUrl iconUrl)
        {
            return new(username, instance, accessToken, iconUrl);
        }

        public async Task<IEnumerable<MastodonAccount>> GetMastodonAccounts()
        {
            var data = await _storage.GetMastodonAccountData();
            return data.Select(d => new MastodonAccount(new Username(d.Username), new Instance(d.Instance),
                new MastodonAccessToken(d.AccessToken), new MastodonAccountIconUrl(d.IconUrl)));
        }

        public async Task SaveMastodonAccount(MastodonAccount account)
        {
            await _storage.SaveMastodonAccountData(new MastodonAccountData(account.Username.Value,
                account.Instance.Value,
                account.AccessToken.Token, account.IconUrl.IconUrl));
        }
    }
}
