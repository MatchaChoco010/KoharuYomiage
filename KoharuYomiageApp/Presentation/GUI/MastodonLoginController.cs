using System;
using System.Threading;
using KoharuYomiageApp.UseCase.AddMastodonAccount;
using KoharuYomiageApp.UseCase.AddMastodonAccount.DataObjects;

namespace KoharuYomiageApp.Presentation.GUI
{
    public class MastodonLoginController : IDisposable
    {
        readonly ILoginMastodonAccount _loginMastodonAccount;
        readonly CancellationTokenSource _cancellationTokenSource = new();

        public MastodonLoginController(ILoginMastodonAccount loginMastodonAccount)
        {
            _loginMastodonAccount = loginMastodonAccount;
        }

        public void LoginMastodonAccount(string instanceName)
        {
            _ = _loginMastodonAccount.Login(new LoginInfo(instanceName), _cancellationTokenSource.Token);
        }

        public void Dispose()
        {
            _cancellationTokenSource.Dispose();
        }
    }
}
