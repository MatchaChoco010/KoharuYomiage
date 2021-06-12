using System;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Presentation.Misskey;
using MisskeyApi;

namespace KoharuYomiageApp.Infrastructures.Misskey
{
    public class MisskeyClient : IMisskeyRegisterClient, IMisskeyGetAuthorizeUrl, IMisskeyGetAccessToken
    {
        public async Task<string> RegisterClient(string instance, CancellationToken cancellationToken = new())
        {
            var secrtet = await Api.RegisterApp(instance, cancellationToken);
            return secrtet.Value;
        }

        public async Task<(string, Uri)> GetAuthorizeUri(string hostName, string secret, CancellationToken cancellationToken = new())
        {
            var (sessionToken, authrizeUrl) = await Api.GetAuthorizeUri(hostName, new Secret(secret), cancellationToken);
            return (sessionToken.Value, authrizeUrl);
        }

        public async Task<(string, (string, string, Uri))> GetAccessToken(string instance, string secret, string sessionToken,
            CancellationToken cancellationToken = new())
        {
            var (accessToken, user) = await Api.GetAccessToken(instance, new Secret(secret),
                new SessionToken(sessionToken), cancellationToken);
            return (accessToken.Value, (user.username, user.name, user.avatarUrl));
        }
    }
}
