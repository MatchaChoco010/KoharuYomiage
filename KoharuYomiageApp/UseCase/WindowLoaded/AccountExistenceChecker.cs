using System.Linq;
using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.Repository;
using KoharuYomiageApp.UseCase.WindowLoaded.DataObjects;

namespace KoharuYomiageApp.UseCase.WindowLoaded
{
    public class AccountExistenceChecker : IPushStartButton
    {
        readonly IConnectionManagerRepository _connectionManagerRepository;
        readonly IMakeMastodonConnection _makeMastodonConnection;
        readonly IMastodonAccountRepository _mastodonAccountRepository;
        readonly IStartApp _startApp;
        readonly IStartRegisteringAccount _startRegisteringAccount;

        public AccountExistenceChecker(IConnectionManagerRepository connectionManagerRepository,
            IMastodonAccountRepository mastodonAccountRepository, IStartRegisteringAccount startRegisteringAccount,
            IMakeMastodonConnection makeMastodonConnection, IStartApp startApp)
        {
            _connectionManagerRepository = connectionManagerRepository;
            _mastodonAccountRepository = mastodonAccountRepository;
            _startRegisteringAccount = startRegisteringAccount;
            _makeMastodonConnection = makeMastodonConnection;
            _startApp = startApp;
        }

        public async Task PushStartButton()
        {
            var mastodonAccounts = (await _mastodonAccountRepository.GetAllMastodonAccounts()).ToArray();

            if (mastodonAccounts.Length is 0)
            {
                _startRegisteringAccount.StartRegisteringAccount();
            }
            else
            {
                var connectionManager = _connectionManagerRepository.GetInstance();
                foreach (var account in mastodonAccounts)
                {
                    var connection = _makeMastodonConnection.MakeConnection(
                        new AddReaderInfo(account.AccountIdentifier.Value, account.Username.Value,
                            account.Instance.Value, account.AccessToken.Token));
                    connectionManager.AddConnection(account.AccountIdentifier, connection);
                }

                _startApp.StartApp();
            }
        }
    }
}
