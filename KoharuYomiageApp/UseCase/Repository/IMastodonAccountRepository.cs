using System.Collections.Generic;
using System.Threading.Tasks;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.Account.Mastodon;

namespace KoharuYomiageApp.UseCase.Repository
{
    public interface IMastodonAccountRepository
    {
        Task<MastodonAccount?> FindMastodonAccount(AccountIdentifier identifier);

        MastodonAccount CreateMastodonAccount(Username username, Instance instance, MastodonAccessToken accessToken,
            MastodonAccountIconUrl iconUrl);

        Task SaveMastodonAccount(MastodonAccount accountData);
        Task<IEnumerable<MastodonAccount>> GetAllMastodonAccounts();
    }
}
