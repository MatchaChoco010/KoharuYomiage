using System.Threading.Tasks;

namespace KoharuYomiageApp.Application.AddMastodonAccount.Interfaces
{
    public interface IMastodonApiRegisterClientService
    {
        ValueTask<(string, string)> RegisterClient(string instance);
    }
}
