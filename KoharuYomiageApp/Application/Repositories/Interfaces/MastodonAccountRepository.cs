using System;
using System.Collections.Generic;
using KoharuYomiageApp.Application.Repositories.UseCases;
using KoharuYomiageApp.Entities;

namespace KoharuYomiageApp.Application.Repositories.Interfaces
{
    public class MastodonAccountRepository : IMastodonAccountRepository
    {
        public AuthorizedMastodonAccount FindAuthorizedMastodonAccount(Username username, Instance instance)
        {
            throw new NotImplementedException();
        }

        public AuthorizedMastodonAccount FindOrCreateAuthorizedMastodonAccount(Username username, Instance instance)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AuthorizedMastodonAccount> GetAuthorizedMastodonAccounts()
        {
            throw new NotImplementedException();
        }

        public void SaveAuthorizedMastodonAccount(AuthorizedMastodonAccount account)
        {
            throw new NotImplementedException();
        }
    }
}
