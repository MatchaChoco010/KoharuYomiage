using System;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.Account.Misskey;
using KoharuYomiageApp.Domain.Connection;
using KoharuYomiageApp.Presentation.Misskey;
using KoharuYomiageApp.UseCase.AddMisskeyAccount.DataObjects;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.UseCase.AddMisskeyAccount
{
    public class AddMisskeyAccount : IAddMisskeyAccount
    {
        readonly IMisskeyAccountRepository _accountRepository;
        readonly IConnectionRepository _connectionRepository;
        readonly IGetAuthorizeUrl _getAuthorizeUrl;
        readonly IShowAuthUrl _showAuthUrl;
        readonly IWaitAuthorize _waitAuthorize;
        readonly IGetAccessToken _getAccessToken;
        readonly IMakeMisskeyConnection _makeMisskeyConnection;
        readonly IShowRegisterClientError _showRegisterClientError;
        readonly IShowAuthorizeError _showAuthorizeError;

        public AddMisskeyAccount(IMisskeyAccountRepository accountRepository,
            IConnectionRepository connectionRepository,
            IGetAuthorizeUrl getAuthorizeUrl, IShowAuthUrl showAuthUrl, IWaitAuthorize waitAuthorize,
            IGetAccessToken getAccessToken, IMakeMisskeyConnection makeMisskeyConnection,
            IShowRegisterClientError showRegisterClientError, IShowAuthorizeError showAuthorizeError)
        {
            _accountRepository = accountRepository;
            _connectionRepository = connectionRepository;
            _getAuthorizeUrl = getAuthorizeUrl;
            _showAuthUrl = showAuthUrl;
            _waitAuthorize = waitAuthorize;
            _getAccessToken = getAccessToken;
            _makeMisskeyConnection = makeMisskeyConnection;
            _showRegisterClientError = showRegisterClientError;
            _showAuthorizeError = showAuthorizeError;
        }

        public async Task Login(string host, CancellationToken cancellationToken)
        {
            string sessionToken;
            Uri authorizeUrl;
            try
            {
                (sessionToken, authorizeUrl) = await _getAuthorizeUrl.GetAuthorizeUri(host, cancellationToken);
            }
            catch
            {
                _showRegisterClientError.ShowRegisterClientError();
                throw;
            }

            _showAuthUrl.ShowAuthUrl(authorizeUrl);

            while (true)
            {
                try
                {
                    await _waitAuthorize.WaitAuthorize(cancellationToken);
                    break;
                }
                catch
                {
                    _showAuthorizeError.ShowAuthorizeError();
                }
            }

            string accessTokenData;
            UserData user;
            try
            {
                (accessTokenData, user) =
                    await _getAccessToken.GetAccessToken(host, sessionToken, cancellationToken);
            }
            catch
            {
                _showAuthorizeError.ShowAuthorizeError();
                throw;
            }
            
            var username = new Username(user.Username);
            var instance = new Instance(host);
            var displayName = new DisplayName(user.DisplayName);
            var identifier = new AccountIdentifier(username, instance);
            var accessToken = new MisskeyAccessToken(accessTokenData);
            var iconUrl = new AccountIconUrl(user.IconUrl);

            var account = await _accountRepository.FindMisskeyAccount(identifier, cancellationToken);
            if (account is not null)
            {
                account = account with { AccessToken = accessToken, IconUrl = iconUrl };
            }
            else
            {
                account = new MisskeyAccount(username, instance, displayName, accessToken, iconUrl,
                    new IsReadingPostsFromThisAccount(true));
            }

            await _accountRepository.SaveMisskeyAccount(account, cancellationToken);

            var connection = _makeMisskeyConnection.MakeConnection(account.Username.Value, account.Instance.Value,
                account.AccessToken.Token);
            _connectionRepository.AddConnection(new Connection(account.AccountIdentifier, connection));
        }
    }
}
