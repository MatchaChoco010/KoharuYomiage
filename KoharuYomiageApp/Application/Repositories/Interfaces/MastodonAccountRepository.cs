using System.Collections.Generic;
using System.Threading.Tasks;
using KoharuYomiageApp.Application.Repositories.UseCases;
using KoharuYomiageApp.Entities;

namespace KoharuYomiageApp.Application.Repositories.Interfaces
{
    public class MastodonAccountRepository : IMastodonAccountRepository
    {
        readonly IMastodonAccountStorage _storage;

        public MastodonAccountRepository(IMastodonAccountStorage storage)
        {
            _storage = storage;
        }

        public async ValueTask<MastodonAccount?> FindMastodonAccount(AccountIdentifier identigier)
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

        public async ValueTask<IEnumerable<MastodonAccount>> GetMastodonAccounts()
        {
            // TODO
            await Task.CompletedTask;
            return new MastodonAccount[] { };
        }

        public async ValueTask SaveMastodonAccount(MastodonAccount account)
        {
            await _storage.SaveMastodonAccountData(new MastodonAccountData(account.Username.Value,
                account.Instance.Value,
                account.AccessToken.Token, account.IconUrl.IconUrl));
        }
    }
}
