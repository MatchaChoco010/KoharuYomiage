using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Domain.ReadingText;

namespace KoharuYomiageApp.UseCase.Repository
{
    public interface IReadingTextContainerRepository
    {
        ReadingTextContainer GetContainer();
        KoharuYomiageApp.Domain.ReadingText.ReadingTextContainerSize CreateContainerSize(int size);
        Task<Domain.ReadingText.ReadingTextContainerSize> GetContainerSize(CancellationToken cancellationToken);
        Task SaveContainerSize(Domain.ReadingText.ReadingTextContainerSize size, CancellationToken cancellationToken);
    }
}
