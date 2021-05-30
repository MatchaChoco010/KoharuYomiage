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

        public AllAccountsProvider(IMastodonAccountRepository mastodonAccountRepository)
        {
            _mastodonAccountRepository = mastodonAccountRepository;
        }

        public async Task<IEnumerable<AccountData>> GetAllAccounts(CancellationToken cancellationToken)
        {
            var data = await _mastodonAccountRepository.GetAllMastodonAccounts(cancellationToken);
            return data.Select(d =>
                    new AccountData(d.AccountIdentifier.Value, d.Username.Value, d.Instance.Value, d.IconUrl.IconUrl))
                .ToList();
        }
    }
}
