using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.UseCase.ReadingTextContainerSize
{
    public interface IChangeReadingTextContainerSize
    {
        Task ChangeContainerSize(int size, CancellationToken cancellationToken);
    }
}
