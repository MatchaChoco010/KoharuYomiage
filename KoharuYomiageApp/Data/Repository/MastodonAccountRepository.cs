using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Data.Repository.DataObjects;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.Account.Mastodon;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.Data.Repository
{
    public class MastodonAccountRepository : IMastodonAccountRepository
    {
        readonly IMastodonAccountStorage _storage;

        public MastodonAccountRepository(IMastodonAccountStorage storage)
        {
            _storage = storage;
        }

        public async Task<MastodonAccount?> FindMastodonAccount(AccountIdentifier identifier,
            CancellationToken cancellationToken)
        {
            var data = await _storage.FindMastodonAccountData(identifier.Value, cancellationToken);
            if (data is not null)
            {
                return new MastodonAccount(new Username(data.Username), new Instance(data.Instance),
                    new DisplayName(data.DisplayName), new MastodonAccessToken(data.AccessToken),
                    new MastodonAccountIconUrl(data.IconUrl),
                    new IsReadingPostsFromThisAccount(data.IsReadingPostsFromThisAccount));
            }

            return null;
        }

        public MastodonAccount CreateMastodonAccount(Username username, Instance instance, DisplayName displayName,
            MastodonAccessToken accessToken, MastodonAccountIconUrl iconUrl)
        {
            return new(username, instance, displayName, accessToken, iconUrl,
                new IsReadingPostsFromThisAccount(true));
        }

        public async Task SaveMastodonAccount(MastodonAccount accountData, CancellationToken cancellationToken)
        {
            await _storage.SaveMastodonAccountData(new MastodonAccountData(accountData.Username.Value,
                accountData.Instance.Value, accountData.DisplayName.Value, accountData.AccessToken.Token,
                accountData.IconUrl.IconUrl, accountData.IsReadingPostsFromThisAccount.Value), cancellationToken);
        }

        public async Task DeleteMastodonAccount(AccountIdentifier identifier, CancellationToken cancellationToken)
        {
            await _storage.DeleteMastodonAccountData(identifier.Value, cancellationToken);
        }

        public async Task<IEnumerable<MastodonAccount>> GetAllMastodonAccounts(CancellationToken cancellationToken)
        {
            var data = await _storage.GetMastodonAccountData(cancellationToken);
            return data.Select(d => new MastodonAccount(new Username(d.Username), new Instance(d.Instance),
                new DisplayName(d.DisplayName), new MastodonAccessToken(d.AccessToken),
                new MastodonAccountIconUrl(d.IconUrl),
                new IsReadingPostsFromThisAccount(d.IsReadingPostsFromThisAccount)));
        }
    }
}
