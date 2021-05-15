using System.Threading.Tasks;
using KoharuYomiageApp.Application.AddMastodonAccount.UseCases;
using KoharuYomiageApp.Application.AddMastodonAccount.UseCases.DataObjects;

namespace KoharuYomiageApp.Application.AddMastodonAccount.Interfaces
{
    public class RegisterClientPresenter : IRegisterClient
    {
        readonly IMastodonApiRegisterClientService _mastodonApi;

        public RegisterClientPresenter(IMastodonApiRegisterClientService mastodonApi)
        {
            _mastodonApi = mastodonApi;
        }

        public async Task<ClientInfo> RegisterClient(LoginInfo loginInfo)
        {
            var (id, secret) = await _mastodonApi.RegisterClient(loginInfo.Instance);
            return new ClientInfo(id, secret);
        }
    }
}
