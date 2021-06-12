using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.UseCase.DeleteAccount
{
    public class AccountDeleter : IDeleteAccount
    {
        readonly IMastodonAccountRepository _mastodonAccountRepository;
        readonly IMisskeyAccountRepository _misskeyAccountRepository;
        readonly IConnectionRepository _connectionRepository;

        public AccountDeleter(IMastodonAccountRepository mastodonAccountRepository,
            IMisskeyAccountRepository misskeyAccountRepository,
            IConnectionRepository connectionRepository)
        {
            _mastodonAccountRepository = mastodonAccountRepository;
            _misskeyAccountRepository = misskeyAccountRepository;
            _connectionRepository = connectionRepository;
        }

        public async Task DeleteAccount(string username, string instance, CancellationToken cancellationToken)
        {
            var id = new AccountIdentifier(new Username(username), new Instance(instance));
            await _mastodonAccountRepository.DeleteMastodonAccount(id, cancellationToken);
            await _misskeyAccountRepository.DeleteMisskeyAccount(id, cancellationToken);
            _connectionRepository.StopConnection(id);
        }
    }
}
