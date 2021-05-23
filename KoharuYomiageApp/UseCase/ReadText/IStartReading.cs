using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.UseCase.ReadText
{
    public interface IStartReading
    {
        Task StartReading(CancellationToken cancellationToken);
    }
}
