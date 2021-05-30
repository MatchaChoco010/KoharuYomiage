using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.Connection;
using KoharuYomiageApp.UseCase.Repository;
using KoharuYomiageApp.UseCase.Utils;

namespace KoharuYomiageApp.UseCase.SwitchAccountConnection
{
    public class AccountConnectionSwitcher : ISwitchAccountConnection
    {
        readonly IConnectionRepository _connectionRepository;
        readonly IMakeMastodonConnection _makeMastodonConnection;
        readonly IMastodonAccountRepository _mastodonAccountRepository;

        public AccountConnectionSwitcher(IMastodonAccountRepository mastodonAccountRepository,
            IConnectionRepository connectionRepository, IMakeMastodonConnection makeMastodonConnection)
        {
            _mastodonAccountRepository = mastodonAccountRepository;
            _connectionRepository = connectionRepository;
            _makeMastodonConnection = makeMastodonConnection;
        }

        public async Task SwitchAccountConnection(string username, string instance, bool connect,
            CancellationToken cancellationToken)
        {
            var id = new AccountIdentifier(new Username(username), new Instance(instance));
            var account = await _mastodonAccountRepository.FindMastodonAccount(id, cancellationToken);

            if (account is null)
            {
                return;
            }

            if (connect)
            {
                var connection = _makeMastodonConnection.MakeConnection(account.Username.Value, account.Instance.Value,
                    account.AccessToken.Token);
                _connectionRepository.AddConnection(new Connection(id, connection));
            }
            else
            {
                _connectionRepository.StopConnection(id);
            }

            account.IsReadingPostsFromThisAccount = new IsReadingPostsFromThisAccount(connect);
            await _mastodonAccountRepository.SaveMastodonAccount(account, cancellationToken);
        }
    }
}
