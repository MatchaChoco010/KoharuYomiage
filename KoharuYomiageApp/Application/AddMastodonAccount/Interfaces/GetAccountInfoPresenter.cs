using System.Threading.Tasks;
using KoharuYomiageApp.Application.AddMastodonAccount.UseCases;
using KoharuYomiageApp.Application.AddMastodonAccount.UseCases.DataObjects;

namespace KoharuYomiageApp.Application.AddMastodonAccount.Interfaces
{
    public class GetAccountInfoPresenter : IGetAccountInfo
    {
        readonly IMastodonApiGetAccountInfoService _getAccountInfoService;

        public GetAccountInfoPresenter(IMastodonApiGetAccountInfoService getAccountInfoService)
        {
            _getAccountInfoService = getAccountInfoService;
        }

        public async ValueTask<AccountInfo> GetAccountInfo(AccessInfo accessInfo)
        {
            var (username, iconUrl) =
                await _getAccountInfoService.GetAccountInfo(accessInfo.instance, accessInfo.Token);
            return new AccountInfo(username, iconUrl);
        }
    }
}
