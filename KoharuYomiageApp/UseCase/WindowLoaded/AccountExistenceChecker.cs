using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Data.Repository;
using KoharuYomiageApp.Domain.Connection;
using KoharuYomiageApp.UseCase.Repository;
using KoharuYomiageApp.UseCase.Utils;

namespace KoharuYomiageApp.UseCase.WindowLoaded
{
    public class AccountExistenceChecker : IPushStartButton
    {
        readonly IConnectionRepository _connectionRepository;
        readonly IMakeMastodonConnection _makeMastodonConnection;
        readonly IMastodonAccountRepository _mastodonAccountRepository;
        readonly IMakeMisskeyConnection _makeMisskeyConnection;
        readonly IMisskeyAccountRepository _misskeyAccountRepository;
        readonly IStartApp _startApp;
        readonly IStartRegisteringAccount _startRegisteringAccount;

        public AccountExistenceChecker(IConnectionRepository connectionRepository,
            IStartRegisteringAccount startRegisteringAccount, IStartApp startApp,
            IMastodonAccountRepository mastodonAccountRepository, IMakeMastodonConnection makeMastodonConnection,
            IMisskeyAccountRepository misskeyAccountRepository, IMakeMisskeyConnection makeMisskeyConnection)
        {
            _connectionRepository = connectionRepository;
            _startRegisteringAccount = startRegisteringAccount;
            _startApp = startApp;
            _mastodonAccountRepository = mastodonAccountRepository;
            _makeMastodonConnection = makeMastodonConnection;
            _misskeyAccountRepository = misskeyAccountRepository;
            _makeMisskeyConnection = makeMisskeyConnection;
        }

        public async Task PushStartButton(CancellationToken cancellationToken)
        {
            var mastodonAccounts =
                (await _mastodonAccountRepository.GetAllMastodonAccounts(cancellationToken)).ToArray();
            var misskeyAccounts =
                (await _misskeyAccountRepository.GetAllMisskeyAccounts(cancellationToken)).ToArray();

            if (mastodonAccounts.Length is 0 && misskeyAccounts.Length is 0)
            {
                _startRegisteringAccount.StartRegisteringAccount();
            }
            else
            {
                foreach (var account in mastodonAccounts)
                {
                    if (!account.IsReadingPostsFromThisAccount.Value)
                    {
                        continue;
                    }

                    var connection = _makeMastodonConnection.MakeConnection(account.Username.Value,
                            account.Instance.Value, account.AccessToken.Token);
                    _connectionRepository.AddConnection(new Connection(account.AccountIdentifier, connection));
                }

                foreach (var account in misskeyAccounts)
                {
                    if (!account.IsReadingPostsFromThisAccount.Value)
                    {
                        continue;
                    }

                    var connection = _makeMisskeyConnection.MakeConnection(account.Username.Value,
                        account.Instance.Value, account.AccessToken.Token);
                    _connectionRepository.AddConnection(new Connection(account.AccountIdentifier, connection));
                }

                _startApp.StartApp();
            }
        }
    }
}
