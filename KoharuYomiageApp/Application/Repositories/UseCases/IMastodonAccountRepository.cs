using System.Collections.Generic;
using System.Threading.Tasks;
using KoharuYomiageApp.Entities;
using KoharuYomiageApp.Entities.Account;
using KoharuYomiageApp.Entities.Account.Mastodon;

namespace KoharuYomiageApp.Application.Repositories.UseCases
{
    public interface IMastodonAccountRepository
    {
        ValueTask<MastodonAccount?> FindMastodonAccount(AccountIdentifier identifier);

        MastodonAccount CreateMastodonAccount(Username username, Instance instance, MastodonAccessToken accessToken,
            MastodonAccountIconUrl iconUrl);

        ValueTask<IEnumerable<MastodonAccount>> GetMastodonAccounts();

        ValueTask SaveMastodonAccount(MastodonAccount account);
    }
}
