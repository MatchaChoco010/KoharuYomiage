using System.Threading.Tasks;
using KoharuYomiageApp.Application.AddMastodonAccount.UseCases;
using KoharuYomiageApp.Application.AddMastodonAccount.UseCases.DataObjects;

namespace KoharuYomiageApp.Application.AddMastodonAccount.Interfaces
{
    public class AuthorizeMastodonAccountWithCodePresenter : IAuthorizeMastodonAccountWithCode
    {
        readonly IMastodonApiAuthorizeAccountWithCodeService _authorizeAccountWithCodeService;

        public AuthorizeMastodonAccountWithCodePresenter(
            IMastodonApiAuthorizeAccountWithCodeService authorizeAccountWithCodeService)
        {
            _authorizeAccountWithCodeService = authorizeAccountWithCodeService;
        }

        public async Task<AccessInfo> AuthorizeMastodonAccountWithCode(AuthorizationInfo authorizationInfo)
        {
            var accessToken =
                await _authorizeAccountWithCodeService.AuthorizeWithCode(authorizationInfo.Instance,
                    authorizationInfo.ClientId, authorizationInfo.ClientSecret, authorizationInfo.AuthorizationCode);
            return new AccessInfo(authorizationInfo.Instance, accessToken);
        }
    }
}
