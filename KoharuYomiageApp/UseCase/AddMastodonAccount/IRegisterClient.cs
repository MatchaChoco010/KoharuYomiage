using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.AddMastodonAccount.DataObjects;

namespace KoharuYomiageApp.UseCase.AddMastodonAccount
{
    public interface IRegisterClient
    {
        Task<ClientInfo> RegisterClient(LoginInfo loginInfo);
    }
}
