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
        readonly IMakeMisskeyConnection _makeMisskeyConnection;
        readonly IMisskeyAccountRepository _misskeyAccountRepository;

        public AccountConnectionSwitcher(IMastodonAccountRepository mastodonAccountRepository,
            IMisskeyAccountRepository misskeyAccountRepository, IConnectionRepository connectionRepository,
            IMakeMastodonConnection makeMastodonConnection, IMakeMisskeyConnection makeMisskeyConnection)
        {
            _mastodonAccountRepository = mastodonAccountRepository;
            _misskeyAccountRepository = misskeyAccountRepository;
            _connectionRepository = connectionRepository;
            _makeMastodonConnection = makeMastodonConnection;
            _makeMisskeyConnection = makeMisskeyConnection;
        }

        public async Task SwitchAccountConnection(string username, string instance, bool connect,
            CancellationToken cancellationToken)
        {
            var id = new AccountIdentifier(new Username(username), new Instance(instance));

            var mastodonAccount = await _mastodonAccountRepository.FindMastodonAccount(id, cancellationToken);
            if (mastodonAccount is not null)
            {
                if (connect)
                {
                    var connection = _makeMastodonConnection.MakeConnection(mastodonAccount.Username.Value, mastodonAccount.Instance.Value,
                        mastodonAccount.AccessToken.Token);
                    _connectionRepository.AddConnection(new Connection(id, connection));
                }
                else
                {
                    _connectionRepository.StopConnection(id);
                }

                mastodonAccount.IsReadingPostsFromThisAccount = new IsReadingPostsFromThisAccount(connect);
                await _mastodonAccountRepository.SaveMastodonAccount(mastodonAccount, cancellationToken);

                return;
            }

            var misskeyAccount = await _misskeyAccountRepository.FindMisskeyAccount(id, cancellationToken);
            if (misskeyAccount is not null)
            {
                if (connect)
                {
                    var connection = _makeMisskeyConnection.MakeConnection(misskeyAccount.Username.Value,
                        misskeyAccount.Instance.Value, misskeyAccount.AccessToken.Token);
                    _connectionRepository.AddConnection(new Connection(id, connection));
                }
                else
                {
                    _connectionRepository.StopConnection(id);
                }

                misskeyAccount.IsReadingPostsFromThisAccount = new IsReadingPostsFromThisAccount(connect);
                await _misskeyAccountRepository.SaveMisskeyAccount(misskeyAccount, cancellationToken);

                return;
            }
        }
    }
}
