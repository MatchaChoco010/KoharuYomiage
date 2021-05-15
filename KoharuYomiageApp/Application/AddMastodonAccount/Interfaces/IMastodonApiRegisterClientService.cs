using System.Threading.Tasks;

namespace KoharuYomiageApp.Application.AddMastodonAccount.Interfaces
{
    public interface IMastodonApiRegisterClientService
    {
        Task<(string, string)> RegisterClient(string instance);
    }
}
