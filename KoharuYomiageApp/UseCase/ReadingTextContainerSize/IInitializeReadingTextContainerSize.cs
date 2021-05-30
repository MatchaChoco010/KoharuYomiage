using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.UseCase.ReadingTextContainerSize
{
    public interface IInitializeReadingTextContainerSize
    {
        Task InitializeReadingTextContainerSize(CancellationToken cancellationToken);
    }
}
