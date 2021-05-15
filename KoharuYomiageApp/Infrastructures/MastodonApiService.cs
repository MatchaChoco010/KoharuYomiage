using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KoharuYomiageApp.Application.AddMastodonAccount.Interfaces;
using MastodonApi;
using IMastodonApiAddAccountToReaderService =
    KoharuYomiageApp.Application.WindowLoaded.Interfaces.IMastodonApiAddAccountToReaderService;

namespace KoharuYomiageApp.Infrastructures
{
    public class MastodonApiService : IMastodonApiAddAccountToReaderService, IMastodonApiRegisterClientService,
        IMastodonApiAuthorizeAccountWithCodeService,
        IMastodonApiGetAccountInfoService,
        Application.AddMastodonAccount.Interfaces.IMastodonApiAddAccountToReaderService
    {
        readonly Dictionary<string, IDisposable> _connections = new();

        public void AddAccountToReader(string accountIdentifier, string instance, string accessToken)
        {
            if (_connections.ContainsKey(accountIdentifier))
            {
                _connections[accountIdentifier].Dispose();
                _connections.Remove(accountIdentifier);
            }

            var disposable =
                Api.GetUserStreamingObservable(instance, new AccessToken(accessToken))
                    .Subscribe(_ => { });
            _connections.Add(accountIdentifier, disposable);
        }

        public async Task<string> AuthorizeWithCode(string instance, string clientId, string clientSecret,
            string code)
        {
            var accessToken =
                await Api.AuthorizeWithCode(instance, new ClientId(clientId), new ClientSecret(clientSecret), code);
            return accessToken.Token;
        }

        public async Task<(string, Uri)> GetAccountInfo(string instance, string accessToken)
        {
            var account = await Api.GetAccountInformation(instance, new AccessToken(accessToken));
            return (account.username, new Uri(account.avatar_static));
        }

        public async Task<(string, string)> RegisterClient(string instance)
        {
            var (id, secret) = await Api.RegisterApp(instance);
            return (id.Id, secret.Secret);
        }
    }
}
