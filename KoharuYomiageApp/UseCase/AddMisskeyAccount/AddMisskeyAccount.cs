﻿using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.Account.Misskey;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.UseCase.AddMisskeyAccount
{
    public class AddMisskeyAccount : IAddMisskeyAccount
    {
        readonly IMisskeyAccountRepository _accountRepository;
        readonly IRegisterClient _registerClient;
        readonly IGetAuthorizeUrl _getAuthorizeUrl;
        readonly IShowAuthUrl _showAuthUrl;
        readonly IWaitAuthorize _waitAuthorize;
        readonly IGetAccessToken _getAccessToken;
        readonly IShowRegisterClientError _showRegisterClientError;
        readonly IShowAuthorizeError _showAuthorizeError;

        public AddMisskeyAccount(IMisskeyAccountRepository accountRepository,
            IRegisterClient registerClient, IGetAuthorizeUrl getAuthorizeUrl,
            IShowAuthUrl showAuthUrl, IWaitAuthorize waitAuthorize, IGetAccessToken getAccessToken,
            IShowRegisterClientError showRegisterClientError, IShowAuthorizeError showAuthorizeError)
        {
            _accountRepository = accountRepository;
            _registerClient = registerClient;
            _getAuthorizeUrl = getAuthorizeUrl;
            _showAuthUrl = showAuthUrl;
            _waitAuthorize = waitAuthorize;
            _getAccessToken = getAccessToken;
            _showRegisterClientError = showRegisterClientError;
            _showAuthorizeError = showAuthorizeError;
        }

        public async Task Login(string host, CancellationToken cancellationToken)
        {
            string secret;
            try
            {
                secret = await _registerClient.RegisterClient(host, cancellationToken);
            }
            catch
            {
                _showRegisterClientError.ShowRegisterClientError();
                throw;
            }

            var (sessionToken, authorizeUrl) =
                await _getAuthorizeUrl.GetAuthorizeUri(host, secret, cancellationToken);

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

            var (accessTokenData, user) =
                await _getAccessToken.GetAccessToken(host, secret, sessionToken, cancellationToken);

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

            // コネクションを張る
        }
    }
}
