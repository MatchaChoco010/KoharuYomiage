using KoharuYomiageApp.Application.AddMastodonAccount.UseCases;
using KoharuYomiageApp.Application.AddMastodonAccount.UseCases.DataObjects;

namespace KoharuYomiageApp.Application.AddMastodonAccount.Interfaces
{
    public class AuthorizeMastodonAccountController
    {
        readonly IAuthorizeMastodonAccount _authorizeMastodonAccount;

        public AuthorizeMastodonAccountController(IAuthorizeMastodonAccount authorizeMastodonAccount)
        {
            _authorizeMastodonAccount = authorizeMastodonAccount;
        }

        public void AuthorizeMastodonAccount(string instance, string authorizationCode)
        {
            _ = _authorizeMastodonAccount.Authorize(new InstanceAndAuthenticationCode(instance, authorizationCode));
        }
    }
}
