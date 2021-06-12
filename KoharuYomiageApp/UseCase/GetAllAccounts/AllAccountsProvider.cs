using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.GetAllAccounts.DataObjects;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.UseCase.GetAllAccounts
{
    public class AllAccountsProvider : IGetAllAccounts
    {
        readonly IMastodonAccountRepository _mastodonAccountRepository;
        readonly IMisskeyAccountRepository _misskeyAccountRepository;

        public AllAccountsProvider(IMastodonAccountRepository mastodonAccountRepository,
            IMisskeyAccountRepository misskeyAccountRepository)
        {
            _mastodonAccountRepository = mastodonAccountRepository;
            _misskeyAccountRepository = misskeyAccountRepository;
        }

        public async Task<IEnumerable<AccountData>> GetAllAccounts(CancellationToken cancellationToken)
        {
            var mastodonData = await _mastodonAccountRepository.GetAllMastodonAccounts(cancellationToken);
            var misskeyData = await _misskeyAccountRepository.GetAllMisskeyAccounts(cancellationToken);
            return mastodonData.Select(d =>
                    new AccountData(d.AccountIdentifier.Value, d.Username.Value, d.Instance.Value, d.IconUrl.IconUrl,
                        d.IsReadingPostsFromThisAccount.Value))
                .Concat(misskeyData.Select(d =>
                    new AccountData(d.AccountIdentifier.Value, d.Username.Value, d.Instance.Value, d.IconUrl.IconUrl,
                        d.IsReadingPostsFromThisAccount.Value)))
                .ToList();
        }
    }
}
