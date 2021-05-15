using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.Application.ReadText.UseCases
{
    public interface IStartReading
    {
        Task StartReading(CancellationToken cancellationToken);
    }
}
