using System.Threading.Tasks;
using KoharuYomiageApp.Application.AddMastodonAccount.UseCases.DataObjects;

namespace KoharuYomiageApp.Application.AddMastodonAccount.UseCases
{
    public interface IRegisterClient
    {
        Task<ClientInfo> RegisterClient(LoginInfo loginInfo);
    }
}
