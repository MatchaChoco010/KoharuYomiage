using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.Account.Mastodon;

namespace KoharuYomiageApp.UseCase.Repository
{
    public interface IMastodonAccountRepository
    {
        Task<MastodonAccount?> FindMastodonAccount(AccountIdentifier identifier, CancellationToken cancellationToken);
        MastodonAccount CreateMastodonAccount(Username username, Instance instance, DisplayName displayName,
            MastodonAccessToken accessToken, MastodonAccountIconUrl iconUrl);
        Task SaveMastodonAccount(MastodonAccount accountData, CancellationToken cancellationToken);
        Task DeleteMastodonAccount(AccountIdentifier accountIdentifier, CancellationToken cancellationToken);
        Task<IEnumerable<MastodonAccount>> GetAllMastodonAccounts(CancellationToken cancellationToken);
    }
}
