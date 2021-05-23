using System.Threading.Tasks;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.Account.Mastodon;
using KoharuYomiageApp.Domain.Connection;
using KoharuYomiageApp.UseCase.AddMastodonAccount.DataObjects;
using KoharuYomiageApp.UseCase.Repository;

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

        public async Task Authorize(InstanceAndAuthenticationCode instanceAndAuthenticationCode)
        {
            var instance = new Instance(instanceAndAuthenticationCode.Instance);

            AccessInfo accessInfo;
            try
            {
                var client = await _mastodonClientRepository.FindMastodonClient(instance);
                if (client is null)
                {
                    _showMastodonAuthenticationError.ShowMastodonAuthenticationError();
                    return;
                }

                var authorizationInfo = new AuthorizationInfo(instance.Value, client.ClientId.Value,
                    client.ClientSecret.Value, instanceAndAuthenticationCode.AuthenticationCode);
                accessInfo =
                    await _authorizeMastodonAccountWithCode.AuthorizeMastodonAccountWithCode(authorizationInfo);
            }
            catch
            {
                _showMastodonAuthenticationError.ShowMastodonAuthenticationError();
                return;
            }

            AccountInfo accountInfo;
            try
            {
                accountInfo = await _getAccountInfo.GetAccountInfo(accessInfo);
            }
            catch
            {
                _showGetMastodonAccountInfoError.ShowGetMastodonAccountInfoError();
                return;
            }

            var username = new Username(accountInfo.Username);
            var accessToken = new MastodonAccessToken(accessInfo.Token);
            var iconUrl = new MastodonAccountIconUrl(accountInfo.IconUrl);

            var account =
                await _mastodonAccountRepository.FindMastodonAccount(new AccountIdentifier(username, instance));
            if (account is null)
            {
                account = _mastodonAccountRepository.CreateMastodonAccount(username, instance, accessToken, iconUrl);
            }
            else
            {
                account = account with {AccessToken = accessToken, IconUrl = iconUrl};
            }

            await _mastodonAccountRepository.SaveMastodonAccount(account);

            var connection = _makeMastodonConnection.MakeConnection(new AddReaderInfo(account.AccountIdentifier.Value,
                account.Username.Value, account.Instance.Value, account.AccessToken.Token));

            _connectionRepository.AddConnection(new Connection(account.AccountIdentifier, connection));

            _finishAuthorizeMastodonAccount.FinishAuthorizeMastodonAccount();
        }
    }
}
