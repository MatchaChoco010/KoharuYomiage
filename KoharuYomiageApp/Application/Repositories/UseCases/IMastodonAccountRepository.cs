using System.Collections.Generic;
using System.Threading.Tasks;
using KoharuYomiageApp.Entities;
using KoharuYomiageApp.Entities.Account;
using KoharuYomiageApp.Entities.Account.Mastodon;

namespace KoharuYomiageApp.Application.Repositories.UseCases
{
    public interface IMastodonAccountRepository
    {
        Task<MastodonAccount?> FindMastodonAccount(AccountIdentifier identifier);

        MastodonAccount CreateMastodonAccount(Username username, Instance instance, MastodonAccessToken accessToken,
            MastodonAccountIconUrl iconUrl);

        Task<IEnumerable<MastodonAccount>> GetMastodonAccounts();

        Task SaveMastodonAccount(MastodonAccount account);
    }
}
