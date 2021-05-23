using KoharuYomiageApp.UseCase.AddMastodonAccount;
using KoharuYomiageApp.UseCase.AddMastodonAccount.DataObjects;

namespace KoharuYomiageApp.Presentation.GUI
{
    public class MastodonLoginController
    {
        readonly ILoginMastodonAccount _loginMastodonAccount;

        public MastodonLoginController(ILoginMastodonAccount loginMastodonAccount)
        {
            _loginMastodonAccount = loginMastodonAccount;
        }

        public void LoginMastodonAccount(string instanceName)
        {
            _ = _loginMastodonAccount.Login(new LoginInfo(instanceName));
        }
    }
}
