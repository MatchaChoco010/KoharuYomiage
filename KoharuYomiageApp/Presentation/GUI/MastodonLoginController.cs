using System;
using System.Threading;
using KoharuYomiageApp.UseCase.AddMastodonAccount;
using KoharuYomiageApp.UseCase.AddMastodonAccount.DataObjects;

namespace KoharuYomiageApp.Presentation.GUI
{
    public class MastodonLoginController : IDisposable
    {
        readonly CancellationTokenSource _cancellationTokenSource = new();
        readonly ILoginMastodonAccount _loginMastodonAccount;

        public MastodonLoginController(ILoginMastodonAccount loginMastodonAccount)
        {
            _loginMastodonAccount = loginMastodonAccount;
        }

        public void Dispose()
        {
            _cancellationTokenSource.Dispose();
        }

        public void LoginMastodonAccount(string instanceName)
        {
            _ = _loginMastodonAccount.Login(new LoginInfo(instanceName), _cancellationTokenSource.Token);
        }
    }
}
