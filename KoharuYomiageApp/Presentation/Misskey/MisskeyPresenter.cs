using System;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.AddMisskeyAccount;
using KoharuYomiageApp.UseCase.AddMisskeyAccount.DataObjects;

namespace KoharuYomiageApp.Presentation.Misskey
{
    public class MisskeyPresenter : IGetAuthorizeUrl, IGetAccessToken, UseCase.Utils.IMakeMisskeyConnection
    {
        readonly IMisskeyGetAuthorizeUrl _getAuthorizeUrl;
        readonly IMisskeyGetAccessToken _getAccessToken;
        readonly IMakeMisskeyConnection _connection;

        public MisskeyPresenter(IMisskeyGetAuthorizeUrl getAuthorizeUrl, IMisskeyGetAccessToken getAccessToken, IMakeMisskeyConnection connection)
        {
            _getAuthorizeUrl = getAuthorizeUrl;
            _getAccessToken = getAccessToken;
            _connection = connection;
        }

        public async Task<(string, Uri)> GetAuthorizeUri(string hostName, CancellationToken cancellationToken)
        {
            return await _getAuthorizeUrl.GetAuthorizeUri(hostName, cancellationToken);
        }

        public async Task<(string, UserData)> GetAccessToken(string instance, string sessionToken,
            CancellationToken cancellationToken)
        {
            var (accessToken, (username, displayName, iconUrl)) =
                await _getAccessToken.GetAccessToken(instance, sessionToken, cancellationToken);
            return (accessToken, new UserData(username, displayName, iconUrl));
        }

        public IDisposable MakeConnection(string username, string instance, string accessToken)
        {
            return _connection.MakeConnection(username, instance, accessToken);
        }
    }
}
