﻿using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.Account.Mastodon;
using KoharuYomiageApp.Domain.Connection;
using KoharuYomiageApp.UseCase.AddMastodonAccount.DataObjects;
using KoharuYomiageApp.UseCase.Repository;
using KoharuYomiageApp.UseCase.Utils;

namespace KoharuYomiageApp.UseCase.AddMastodonAccount
{
    public class AuthorizeMastodonAccount : IAuthorizeMastodonAccount
    {
        readonly IAuthorizeMastodonAccountWithCode _authorizeMastodonAccountWithCode;
        readonly IConnectionRepository _connectionRepository;
        readonly IFinishAuthorizeMastodonAccount _finishAuthorizeMastodonAccount;
        readonly IGetAccountInfo _getAccountInfo;
        readonly IMakeMastodonConnection _makeMastodonConnection;
        readonly IMastodonAccountRepository _mastodonAccountRepository;
        readonly IMastodonClientRepository _mastodonClientRepository;
        readonly IShowGetMastodonAccountInfoError _showGetMastodonAccountInfoError;
        readonly IShowMastodonAuthenticationError _showMastodonAuthenticationError;

        public AuthorizeMastodonAccount(IConnectionRepository connectionRepository,
            IMastodonClientRepository mastodonClientRepository,
            IMastodonAccountRepository mastodonAccountRepository,
            IAuthorizeMastodonAccountWithCode authorizeMastodonAccountWithCode,
            IShowMastodonAuthenticationError showMastodonAuthenticationError,
            IGetAccountInfo getAccountInfo,
            IShowGetMastodonAccountInfoError showGetMastodonAccountInfoError,
            IMakeMastodonConnection makeMastodonConnection,
            IFinishAuthorizeMastodonAccount finishAuthorizeMastodonAccount)
        {
            _connectionRepository = connectionRepository;
            _mastodonClientRepository = mastodonClientRepository;
            _mastodonAccountRepository = mastodonAccountRepository;
            _authorizeMastodonAccountWithCode = authorizeMastodonAccountWithCode;
            _showMastodonAuthenticationError = showMastodonAuthenticationError;
            _getAccountInfo = getAccountInfo;
            _showGetMastodonAccountInfoError = showGetMastodonAccountInfoError;
            _makeMastodonConnection = makeMastodonConnection;
            _finishAuthorizeMastodonAccount = finishAuthorizeMastodonAccount;
        }

        public async Task Authorize(InstanceAndAuthenticationCode instanceAndAuthenticationCode,
            CancellationToken cancellationToken)
        {
            var instance = new Instance(instanceAndAuthenticationCode.Instance);

            AccessInfo accessInfo;
            try
            {
                var client = await _mastodonClientRepository.FindMastodonClient(instance, cancellationToken);
                if (client is null)
                {
                    _showMastodonAuthenticationError.ShowMastodonAuthenticationError();
                    return;
                }

                var authorizationInfo = new AuthorizationInfo(instance.Value, client.ClientId.Value,
                    client.ClientSecret.Value, instanceAndAuthenticationCode.AuthenticationCode);
                accessInfo =
                    await _authorizeMastodonAccountWithCode.AuthorizeMastodonAccountWithCode(authorizationInfo,
                        cancellationToken);
            }
            catch
            {
                _showMastodonAuthenticationError.ShowMastodonAuthenticationError();
                return;
            }

            AccountInfo accountInfo;
            try
            {
                accountInfo = await _getAccountInfo.GetAccountInfo(accessInfo, cancellationToken);
            }
            catch
            {
                _showGetMastodonAccountInfoError.ShowGetMastodonAccountInfoError();
                return;
            }

            var username = new Username(accountInfo.Username);
            var displayName = new DisplayName(accountInfo.DisplayName);
            var accessToken = new MastodonAccessToken(accessInfo.Token);
            var iconUrl = new AccountIconUrl(accountInfo.IconUrl);

            var account =
                await _mastodonAccountRepository.FindMastodonAccount(new AccountIdentifier(username, instance),
                    cancellationToken);
            if (account is null)
            {
                account = _mastodonAccountRepository.CreateMastodonAccount(username, instance, displayName, accessToken,
                    iconUrl);
            }
            else
            {
                account = account with {AccessToken = accessToken, IconUrl = iconUrl};
            }

            await _mastodonAccountRepository.SaveMastodonAccount(account, cancellationToken);

            var connection = _makeMastodonConnection.MakeConnection(account.Username.Value, account.Instance.Value,
                account.AccessToken.Token);
            _connectionRepository.AddConnection(new Connection(account.AccountIdentifier, connection));

            _finishAuthorizeMastodonAccount.FinishAuthorizeMastodonAccount();
        }
    }
}
