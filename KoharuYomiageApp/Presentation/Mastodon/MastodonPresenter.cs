using System;
using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.AddMastodonAccount;
using KoharuYomiageApp.UseCase.AddMastodonAccount.DataObjects;

namespace KoharuYomiageApp.Presentation.Mastodon
{
    public class MastodonPresenter : IRegisterClient, IAuthorizeMastodonAccountWithCode, IGetAccountInfo,
        UseCase.AddMastodonAccount.IMakeMastodonConnection, UseCase.WindowLoaded.IMakeMastodonConnection
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

        public async Task<AccessInfo> AuthorizeMastodonAccountWithCode(AuthorizationInfo authorizationInfo)
        {
            var accessToken =
                await _authorizeAccountWithCode.AuthorizeWithCode(authorizationInfo.Instance,
                    authorizationInfo.ClientId, authorizationInfo.ClientSecret, authorizationInfo.AuthorizationCode);
            return new AccessInfo(authorizationInfo.Instance, accessToken);
        }

        public async Task<AccountInfo> GetAccountInfo(AccessInfo accessInfo)
        {
            var (instance, token) = accessInfo;
            var (username, iconUrl) =
                await _getAccountInfo.GetAccountInfo(instance, token);
            return new AccountInfo(username, iconUrl);
        }

        IDisposable UseCase.AddMastodonAccount.IMakeMastodonConnection.MakeConnection(AddReaderInfo info)
        {
            return _connection.MakeConnection(info.Username, info.Instance, info.AccessToken);
        }

        IDisposable UseCase.WindowLoaded.IMakeMastodonConnection.MakeConnection(
            UseCase.WindowLoaded.DataObjects.AddReaderInfo info)
        {
            return _connection.MakeConnection(info.Username, info.Instance, info.AccessToken);
        }

        public async Task<ClientInfo> RegisterClient(LoginInfo loginInfo)
        {
            var (id, secret) = await _registerClient.RegisterClient(loginInfo.Instance);
            return new ClientInfo(id, secret);
        }
    }
}
