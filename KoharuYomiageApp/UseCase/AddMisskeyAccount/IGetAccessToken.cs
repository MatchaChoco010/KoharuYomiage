using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.AddMisskeyAccount.DataObjects;

namespace KoharuYomiageApp.UseCase.AddMisskeyAccount
{
    public interface IGetAccessToken
    {
        Task<(string, UserData)> GetAccessToken(string instance, string secret, string sessionToken,
            CancellationToken cancellationToken);
    }
}
