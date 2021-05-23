using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.UseCase.WindowLoaded
{
    public interface IPushStartButton
    {
        Task PushStartButton(CancellationToken cancellationToken);
    }
}
