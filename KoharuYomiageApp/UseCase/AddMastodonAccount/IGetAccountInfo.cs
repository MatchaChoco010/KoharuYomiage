using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.AddMastodonAccount.DataObjects;

namespace KoharuYomiageApp.UseCase.AddMastodonAccount
{
    public interface IGetAccountInfo
    {
        Task<AccountInfo> GetAccountInfo(AccessInfo accessInfo);
    }
}
