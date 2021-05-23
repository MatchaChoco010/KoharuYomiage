using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.AddMastodonAccount;
using KoharuYomiageApp.UseCase.AddMastodonAccount.DataObjects;
using KoharuYomiageApp.UseCase.AddMastodonTimelineItem;

namespace KoharuYomiageApp.Presentation.Mastodon
{
    public class MastodonPresenter : IRegisterClient, IAuthorizeMastodonAccountWithCode, IGetAccountInfo,
        IAddMastodonAccountToReader, UseCase.WindowLoaded.IAddMastodonAccountToReader
    {
        readonly IMastodonAddAccountToReader _addAccountToReader;
        readonly IMastodonAuthorizeAccountWithCode _authorizeAccountWithCode;
        readonly IMastodonGetAccountInfo _getAccountInfo;
        readonly IMastodonRegisterClient _registerClient;

        public MastodonPresenter(IMastodonRegisterClient registerClient,
            IMastodonAuthorizeAccountWithCode authorizeAccountWithCode, IMastodonGetAccountInfo getAccountInfo,
            IMastodonAddAccountToReader addAccountToReader)
        {
            _registerClient = registerClient;
            _authorizeAccountWithCode = authorizeAccountWithCode;
            _getAccountInfo = getAccountInfo;
            _addAccountToReader = addAccountToReader;
        }

        void IAddMastodonAccountToReader.AddMastodonAccountToReader(AddReaderInfo info)
        {
            _addAccountToReader.AddAccountToReader(info.AccountIdentifier, info.Username, info.Instance,
                info.AccessToken);
        }

        void UseCase.WindowLoaded.IAddMastodonAccountToReader.AddMastodonAccountToReader(
            UseCase.WindowLoaded.DataObjects.AddReaderInfo info)
        {
            _addAccountToReader.AddAccountToReader(info.AccountIdentifier, info.Username, info.Instance,
                info.AccessToken);
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

        public async Task<ClientInfo> RegisterClient(LoginInfo loginInfo)
        {
            var (id, secret) = await _registerClient.RegisterClient(loginInfo.Instance);
            return new ClientInfo(id, secret);
        }
    }
}
