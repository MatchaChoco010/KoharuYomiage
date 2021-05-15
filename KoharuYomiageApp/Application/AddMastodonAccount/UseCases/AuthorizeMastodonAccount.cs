using System.Threading.Tasks;
using KoharuYomiageApp.Application.AddMastodonAccount.UseCases.DataObjects;
using KoharuYomiageApp.Application.Repositories.UseCases;
using KoharuYomiageApp.Entities.Account;
using KoharuYomiageApp.Entities.Account.Mastodon;

namespace KoharuYomiageApp.Application.AddMastodonAccount.UseCases
{
    public class AuthorizeMastodonAccount : IAuthorizeMastodonAccount
    {
        readonly IAddMastodonAccountToReader _addMastodonAccountToReader;
        readonly IAuthorizeMastodonAccountWithCode _authorizeMastodonAccountWithCode;
        readonly IFinishAuthorizeMastodonAccount _finishAuthorizeMastodonAccount;
        readonly IGetAccountInfo _getAccountInfo;
        readonly MastodonAccountRepository _mastodonAccountRepository;
        readonly MastodonClientRepository _mastodonClientRepository;
        readonly IShowGetMastodonAccountInfoError _showGetMastodonAccountInfoError;
        readonly IShowMastodonAuthenticationError _showMastodonAuthenticationError;

        public AuthorizeMastodonAccount(MastodonClientRepository mastodonClientRepository,
            MastodonAccountRepository mastodonAccountRepository,
            IAuthorizeMastodonAccountWithCode authorizeMastodonAccountWithCode,
            IShowMastodonAuthenticationError showMastodonAuthenticationError,
            IGetAccountInfo getAccountInfo,
            IShowGetMastodonAccountInfoError showGetMastodonAccountInfoError,
            IAddMastodonAccountToReader addMastodonAccountToReader,
            IFinishAuthorizeMastodonAccount finishAuthorizeMastodonAccount)
        {
            _mastodonClientRepository = mastodonClientRepository;
            _mastodonAccountRepository = mastodonAccountRepository;
            _authorizeMastodonAccountWithCode = authorizeMastodonAccountWithCode;
            _showMastodonAuthenticationError = showMastodonAuthenticationError;
            _getAccountInfo = getAccountInfo;
            _showGetMastodonAccountInfoError = showGetMastodonAccountInfoError;
            _addMastodonAccountToReader = addMastodonAccountToReader;
            _finishAuthorizeMastodonAccount = finishAuthorizeMastodonAccount;
        }

        public async Task Authorize(InstanceAndAuthenticationCode instanceAndAuthentiacationCode)
        {
            var instance = new Instance(instanceAndAuthentiacationCode.Instance);

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
                    client.ClientSecret.Value, instanceAndAuthentiacationCode.AuthenticationCode);
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

            _addMastodonAccountToReader.AddMastodonAccountToReader(new AddReaderInfo(account.AccountIdentifier.Value,
                account.Instance.Value, account.AccessToken.Token));

            _finishAuthorizeMastodonAccount.FinishAuthorizeMastodonAccount();
        }
    }
}
