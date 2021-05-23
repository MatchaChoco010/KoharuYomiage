using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.AddMastodonAccount.DataObjects;

namespace KoharuYomiageApp.UseCase.AddMastodonAccount
{
    public interface IAuthorizeMastodonAccount
    {
        Task Authorize(InstanceAndAuthenticationCode instanceAndAuthenticationCode);
    }
}
