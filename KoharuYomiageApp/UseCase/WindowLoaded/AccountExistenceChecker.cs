using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Domain.Connection;
using KoharuYomiageApp.UseCase.Repository;
using KoharuYomiageApp.UseCase.WindowLoaded.DataObjects;

namespace KoharuYomiageApp.UseCase.WindowLoaded
{
    public class AccountExistenceChecker : IPushStartButton
    {
        readonly IConnectionRepository _connectionRepository;
        readonly IMakeMastodonConnection _makeMastodonConnection;
        readonly IMastodonAccountRepository _mastodonAccountRepository;
        readonly IStartApp _startApp;
        readonly IStartRegisteringAccount _startRegisteringAccount;

        public AccountExistenceChecker(IConnectionRepository connectionRepository,
            IMastodonAccountRepository mastodonAccountRepository, IStartRegisteringAccount startRegisteringAccount,
            IMakeMastodonConnection makeMastodonConnection, IStartApp startApp)
        {
            _connectionRepository = connectionRepository;
            _mastodonAccountRepository = mastodonAccountRepository;
            _startRegisteringAccount = startRegisteringAccount;
            _makeMastodonConnection = makeMastodonConnection;
            _startApp = startApp;
        }

        public async Task PushStartButton(CancellationToken cancellationToken)
        {
            var mastodonAccounts = (await _mastodonAccountRepository.GetAllMastodonAccounts(cancellationToken)).ToArray();

            if (mastodonAccounts.Length is 0)
            {
                _startRegisteringAccount.StartRegisteringAccount();
            }
            else
            {
                foreach (var account in mastodonAccounts)
                {
                    var connection = _makeMastodonConnection.MakeConnection(
                        new AddReaderInfo(account.AccountIdentifier.Value, account.Username.Value,
                            account.Instance.Value, account.AccessToken.Token));
                    _connectionRepository.AddConnection(new Connection(account.AccountIdentifier, connection));
                }

                _startApp.StartApp();
            }
        }
    }
}
