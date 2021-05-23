using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.AddMastodonAccount.DataObjects;

namespace KoharuYomiageApp.UseCase.AddMastodonAccount
{
    public interface IAuthorizeMastodonAccountWithCode
    {
        Task<AccessInfo> AuthorizeMastodonAccountWithCode(AuthorizationInfo authorizationInfo,
            CancellationToken cancellationToken);
    }
}
