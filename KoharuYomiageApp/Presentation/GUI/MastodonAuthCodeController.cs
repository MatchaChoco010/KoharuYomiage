using System;
using System.Threading;
using KoharuYomiageApp.UseCase.AddMastodonAccount;
using KoharuYomiageApp.UseCase.AddMastodonAccount.DataObjects;

namespace KoharuYomiageApp.Presentation.GUI
{
    public class MastodonAuthCodeController : IDisposable
    {
        readonly IAuthorizeMastodonAccount _authorizeMastodonAccount;
        readonly CancellationTokenSource _cancellationTokenSource = new();

        public MastodonAuthCodeController(IAuthorizeMastodonAccount authorizeMastodonAccount)
        {
            _authorizeMastodonAccount = authorizeMastodonAccount;
        }

        public void Dispose()
        {
            _cancellationTokenSource.Dispose();
        }

        public void AuthorizeMastodonAccount(string instance, string authorizationCode)
        {
            _ = _authorizeMastodonAccount.Authorize(new InstanceAndAuthenticationCode(instance, authorizationCode),
                _cancellationTokenSource.Token);
        }
    }
}
