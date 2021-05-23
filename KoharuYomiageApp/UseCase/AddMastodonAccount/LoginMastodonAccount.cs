using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.Client.Mastodon;
using KoharuYomiageApp.UseCase.AddMastodonAccount.DataObjects;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.UseCase.AddMastodonAccount
{
    public class LoginMastodonAccount : ILoginMastodonAccount
    {
        readonly IMastodonClientRepository _clientRepository;
        readonly IRegisterClient _registerClient;
        readonly IShowAuthUrl _showAuthUrl;
        readonly IShowRegisterClientError _showRegisterClientError;

        public LoginMastodonAccount(IMastodonClientRepository clientRepository, IRegisterClient registerClient,
            IShowAuthUrl showAuthUrl, IShowRegisterClientError showRegisterClientError)
        {
            _clientRepository = clientRepository;
            _registerClient = registerClient;
            _showAuthUrl = showAuthUrl;
            _showRegisterClientError = showRegisterClientError;
        }

        public async Task Login(LoginInfo loginInfo,CancellationToken cancellationToken)
        {
            var instance = new Instance(loginInfo.Instance);

            var mastodonClient = await _clientRepository.FindMastodonClient(instance, cancellationToken);
            if (mastodonClient is null)
            {
                MastodonClientId clientId;
                MastodonClientSecret clientSecret;

                try
                {
                    var (id, secret) = await _registerClient.RegisterClient(loginInfo, cancellationToken);
                    clientId = new MastodonClientId(id);
                    clientSecret = new MastodonClientSecret(secret);
                }
                catch
                {
                    _showRegisterClientError.ShowRegisterClientError();
                    return;
                }

                mastodonClient = _clientRepository.CreateMastodonClient(instance, clientId, clientSecret);
                await _clientRepository.SaveMastodonClient(mastodonClient, cancellationToken);
            }

            var authorizeUrl = await mastodonClient.GetAuthorizeUri();

            _showAuthUrl.ShowAuthUrl(new AuthorizationUrl(authorizeUrl));
        }
    }
}
