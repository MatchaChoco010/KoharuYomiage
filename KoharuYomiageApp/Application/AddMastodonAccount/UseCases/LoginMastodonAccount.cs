using System.Threading.Tasks;
using KoharuYomiageApp.Application.AddMastodonAccount.UseCases.DataObjects;
using KoharuYomiageApp.Application.Repositories.UseCases;
using KoharuYomiageApp.Entities;

namespace KoharuYomiageApp.Application.AddMastodonAccount.UseCases
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

        public async ValueTask Login(LoginInfo loginInfo)
        {
            MastodonClientId clientId;
            MastodonClientSecret clientSecret;

            try
            {
                var (id, secret) = await _registerClient.RegisterClient(loginInfo);
                clientId = new MastodonClientId(id);
                clientSecret = new MastodonClientSecret(secret);
            }
            catch
            {
                _showRegisterClientError.ShowRegisterClientError();
                return;
            }

            var instance = new Instance(loginInfo.Instance);

            var mastodonClient = _clientRepository.CreateMastodonClient(instance, clientId, clientSecret);

            var authorizeUrl = await mastodonClient.GetAuthorizeUri();

            _showAuthUrl.ShowAuthUrl(new AuthUrl(authorizeUrl));
        }
    }
}
