using System.Threading.Tasks;
using KoharuYomiageApp.Application.AddMastodonAccount.Interfaces;
using MastodonApi;

namespace KoharuYomiageApp.Infrastructures
{
    public class MastodonApiService : IMastodonApiRegisterClientService
    {
        public async ValueTask<(string, string)> RegisterClient(string instance)
        {
            var (id, secret) = await Api.RegisterApp(instance);
            return (id.Id, secret.Secret);
        }
    }
}
