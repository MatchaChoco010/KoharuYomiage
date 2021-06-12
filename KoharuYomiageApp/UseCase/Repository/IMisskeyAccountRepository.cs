using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.Account.Misskey;

namespace KoharuYomiageApp.UseCase.Repository
{
    public interface IMisskeyAccountRepository
    {
        Task<MisskeyAccount?> FindMisskeyAccount(AccountIdentifier identifier, CancellationToken cancellationToken);
        MisskeyAccount CreateMisskeyAccount(Username username, Instance instance, DisplayName displayName,
            MisskeyAccessToken accessToken, AccountIconUrl iconUrl);
        Task SaveMisskeyAccount(MisskeyAccount accountData, CancellationToken cancellationToken);
        Task DeleteMisskeyAccount(AccountIdentifier accountIdentifier, CancellationToken cancellationToken);
        Task<IEnumerable<MisskeyAccount>> GetAllMisskeyAccounts(CancellationToken cancellationToken);
    }
}
