using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.AddMastodonAccount.DataObjects;

namespace KoharuYomiageApp.UseCase.AddMastodonAccount
{
    public interface ILoginMastodonAccount
    {
        Task Login(LoginInfo loginInfo, CancellationToken cancellationToken);
    }
}
