using System;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.AddMisskeyAccount;
using KoharuYomiageApp.UseCase.AddMisskeyAccount.DataObjects;

namespace KoharuYomiageApp.Presentation.Misskey
{
    public class MisskeyPresenter : IRegisterClient, IGetAuthorizeUrl, IGetAccessToken
    {
        readonly IMisskeyRegisterClient _registerClient;
        readonly IMisskeyGetAuthorizeUrl _getAuthorizeUrl;
        readonly IMisskeyGetAccessToken _getAccessToken;

        public MisskeyPresenter(IMisskeyRegisterClient registerClient, IMisskeyGetAuthorizeUrl getAuthorizeUrl,
            IMisskeyGetAccessToken getAccessToken)
        {
            _registerClient = registerClient;
            _getAuthorizeUrl = getAuthorizeUrl;
            _getAccessToken = getAccessToken;
        }

        public async Task<string> RegisterClient(string instance, CancellationToken cancellationToken)
        {
            return await _registerClient.RegisterClient(instance, cancellationToken);
        }

        public async Task<(string, Uri)> GetAuthorizeUri(string hostName, string secret,
            CancellationToken cancellationToken)
        {
            return await _getAuthorizeUrl.GetAuthorizeUri(hostName, secret, cancellationToken);
        }

        public async Task<(string, UserData)> GetAccessToken(string instance, string secret, string sessionToken,
            CancellationToken cancellationToken)
        {
            var (accessToken, (username, displayName, iconUrl)) =
                await _getAccessToken.GetAccessToken(instance, secret, sessionToken, cancellationToken);
            return (accessToken, new UserData(username, displayName, iconUrl));
        }
    }
}
