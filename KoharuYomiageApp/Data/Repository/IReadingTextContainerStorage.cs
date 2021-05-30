using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.Data.Repository
{
    public interface IReadingTextContainerStorage
    {
        Task<int?> FindReadingTextContainerSize(CancellationToken cancellationToken);
        Task SaveReadingTextContainerSize(int size, CancellationToken cancellationToken);
    }
}
