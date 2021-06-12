using System.Threading;
using System.Threading.Tasks;
namespace KoharuYomiageApp.UseCase.AddMisskeyAccount
{
    public interface IAuthorizeMisskeyAccount
    {
        Task Authorize(string instance, string secret, string sessionToken, CancellationToken cancellationToken);
    }
}
