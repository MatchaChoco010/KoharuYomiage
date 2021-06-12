using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.UseCase.AddMisskeyAccount
{
    public interface IWaitAuthorize
    {
        Task WaitAuthorize(CancellationToken cancellationToken);
    }
}
