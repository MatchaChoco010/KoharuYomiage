using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.UseCase.GetGlobalVolume
{
    public interface IGetGlobalVolume
    {
        Task<double> GetGlobalVolume(CancellationToken cancellationToken);
    }
}
