using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.UseCase.ReadingTextContainerSize
{
    public interface IGetReadingTextContainerSize
    {
        Task<int> GetReadingTextContainerSize(CancellationToken cancellationToken);
    }
}
