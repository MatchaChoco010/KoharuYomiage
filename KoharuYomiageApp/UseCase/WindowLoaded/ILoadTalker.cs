using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.UseCase.WindowLoaded
{
    public interface ILoadTalker
    {
        Task LoadTalker(CancellationToken cancellationToken);
    }
}
