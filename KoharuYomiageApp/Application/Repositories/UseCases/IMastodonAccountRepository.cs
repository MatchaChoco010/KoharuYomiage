using System.Collections.Generic;
using KoharuYomiageApp.Entities;

namespace KoharuYomiageApp.Application.Repositories.UseCases
{
    public interface IMastodonAccountRepository
    {
        AuthorizedMastodonAccount FindAuthorizedMastodonAccount(Username username, Instance instance);

        AuthorizedMastodonAccount FindOrCreateAuthorizedMastodonAccount(Username username, Instance instance);

        IEnumerable<AuthorizedMastodonAccount> GetAuthorizedMastodonAccounts();

        void SaveAuthorizedMastodonAccount(AuthorizedMastodonAccount account);
    }
}
