using System;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.AddMastodonAccount;
using KoharuYomiageApp.UseCase.AddMastodonAccount.DataObjects;

namespace KoharuYomiageApp.Presentation.Mastodon
{
    public class MastodonPresenter : IRegisterClient, IAuthorizeMastodonAccountWithCode, IGetAccountInfo,
        UseCase.Utils.IMakeMastodonConnection
    {
        readonly IMastodonAuthorizeAccountWithCode _authorizeAccountWithCode;
        readonly IMakeMastodonConnection _connection;
        readonly IMastodonGetAccountInfo _getAccountInfo;
        readonly IMastodonRegisterClient _registerClient;

        public MastodonPresenter(IMastodonRegisterClient registerClient,
            IMastodonAuthorizeAccountWithCode authorizeAccountWithCode, IMastodonGetAccountInfo getAccountInfo,
            IMakeMastodonConnection connection)
        {
            _registerClient = registerClient;
            _authorizeAccountWithCode = authorizeAccountWithCode;
            _getAccountInfo = getAccountInfo;
            _connection = connection;
        }

        public async Task<AccessInfo> AuthorizeMastodonAccountWithCode(AuthorizationInfo authorizationInfo,
            CancellationToken cancellationToken)
        {
            var accessToken =
                await _authorizeAccountWithCode.AuthorizeWithCode(authorizationInfo.Instance,
                    authorizationInfo.ClientId, authorizationInfo.ClientSecret, authorizationInfo.AuthorizationCode,
                    cancellationToken);
            return new AccessInfo(authorizationInfo.Instance, accessToken);
        }

        public async Task<AccountInfo> GetAccountInfo(AccessInfo accessInfo, CancellationToken cancellationToken)
        {
            var (instance, token) = accessInfo;
            var (username, iconUrl) =
                await _getAccountInfo.GetAccountInfo(instance, token, cancellationToken);
            return new AccountInfo(username, iconUrl);
        }

        public async Task<ClientInfo> RegisterClient(LoginInfo loginInfo, CancellationToken cancellationToken)
        {
            var (id, secret) = await _registerClient.RegisterClient(loginInfo.Instance, cancellationToken);
            return new ClientInfo(id, secret);
        }

        public IDisposable MakeConnection(string username, string instance, string accessToken)
        {
            return _connection.MakeConnection(username, instance, accessToken);
        }
    }
}
