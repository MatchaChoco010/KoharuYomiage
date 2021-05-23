using KoharuYomiageApp.UseCase.AddMastodonAccount;
using KoharuYomiageApp.UseCase.AddMastodonAccount.DataObjects;

namespace KoharuYomiageApp.Presentation.GUI
{
    public class MastodonAuthCodeController
    {
        readonly IAuthorizeMastodonAccount _authorizeMastodonAccount;

        public MastodonAuthCodeController(IAuthorizeMastodonAccount authorizeMastodonAccount)
        {
            _authorizeMastodonAccount = authorizeMastodonAccount;
        }

        public void AuthorizeMastodonAccount(string instance, string authorizationCode)
        {
            _ = _authorizeMastodonAccount.Authorize(new InstanceAndAuthenticationCode(instance, authorizationCode));
        }
    }
}
