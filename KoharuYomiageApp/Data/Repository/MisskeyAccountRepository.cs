using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Data.Repository.DataObjects;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.Account.Misskey;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.Data.Repository
{
    public class MisskeyAccountRepository : IMisskeyAccountRepository
    {
        readonly IMisskeyAccountStorage _storage;

        public MisskeyAccountRepository(IMisskeyAccountStorage storage)
        {
            _storage = storage;
        }

        public async Task<MisskeyAccount?> FindMisskeyAccount(AccountIdentifier identifier,
            CancellationToken cancellationToken)
        {
            var data = await _storage.FindMisskeyAccountData(identifier.Value, cancellationToken);
            if (data is not null)
            {
                return new MisskeyAccount(new Username(data.Username), new Instance(data.Instance),
                    new DisplayName(data.DisplayName), new MisskeyAccessToken(data.AccessToken),
                    new AccountIconUrl(data.IconUrl),
                    new IsReadingPostsFromThisAccount(data.IsReadingPostsFromThisAccount));
            }

            return null;
        }

        public MisskeyAccount CreateMisskeyAccount(Username username, Instance instance, DisplayName displayName,
            MisskeyAccessToken accessToken, AccountIconUrl iconUrl)
        {
            return new(username, instance, displayName, accessToken, iconUrl,
                new IsReadingPostsFromThisAccount(true));
        }

        public async Task SaveMisskeyAccount(MisskeyAccount accountData, CancellationToken cancellationToken)
        {
            await _storage.SaveMisskeyAccountData(new MisskeyAccountData(accountData.Username.Value,
                accountData.Instance.Value, accountData.DisplayName.Value, accountData.AccessToken.Token,
                accountData.IconUrl.IconUrl, accountData.IsReadingPostsFromThisAccount.Value), cancellationToken);
        }

        public async Task DeleteMisskeyAccount(AccountIdentifier identifier, CancellationToken cancellationToken)
        {
            await _storage.DeleteMisskeyAccountData(identifier.Value, cancellationToken);
        }

        public async Task<IEnumerable<MisskeyAccount>> GetAllMisskeyAccounts(CancellationToken cancellationToken)
        {
            var data = await _storage.GetMisskeyAccountData(cancellationToken);
            return data.Select(d => new MisskeyAccount(new Username(d.Username), new Instance(d.Instance),
                new DisplayName(d.DisplayName), new MisskeyAccessToken(d.AccessToken),
                new AccountIconUrl(d.IconUrl),
                new IsReadingPostsFromThisAccount(d.IsReadingPostsFromThisAccount)));
        }
    }
}
