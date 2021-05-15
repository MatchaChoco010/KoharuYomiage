using System;
using System.Threading.Tasks;

namespace KoharuYomiageApp.Application.AddMastodonAccount.Interfaces
{
    public interface IMastodonApiGetAccountInfoService
    {
        Task<(string, Uri)> GetAccountInfo(string instance, string accessToken);
    }
}
