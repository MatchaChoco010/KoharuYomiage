using System;
using System.Threading.Tasks;

namespace KoharuYomiageApp.Application.AddMastodonAccount.Interfaces
{
    public interface IMastodonApiGetAccountInfoService
    {
        ValueTask<(string, Uri)> GetAccountInfo(string instance, string accessToken);
    }
}
