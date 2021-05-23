using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.UseCase.WindowLoaded
{
    public interface IWindowLoaded
    {
        Task WindowLoaded(CancellationToken cancellationToken);
    }
}
