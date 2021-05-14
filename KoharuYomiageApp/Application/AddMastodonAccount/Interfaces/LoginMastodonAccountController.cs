using KoharuYomiageApp.Application.AddMastodonAccount.UseCases;
using KoharuYomiageApp.Application.AddMastodonAccount.UseCases.DataObjects;

namespace KoharuYomiageApp.Application.AddMastodonAccount.Interfaces
{
    public class LoginMastodonAccountController
    {
        readonly ILoginMastodonAccount _loginMastodonAccount;

        public LoginMastodonAccountController(ILoginMastodonAccount loginMastodonAccount)
        {
            _loginMastodonAccount = loginMastodonAccount;
        }

        public void LoginMastodonAccount(string instanceName)
        {
            _ = _loginMastodonAccount.Login(new LoginInfo(instanceName));
        }
    }
}
